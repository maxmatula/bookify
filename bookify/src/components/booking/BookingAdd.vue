<template>
    <div>
        <div class="col-12">
            <h1>{{name}}</h1>
        </div>
        <div class="col-sm-12 col-md-8">
            <div v-if="step == 1">
                <p>
                    <b-link :to="$route.matched[0].path">&laquo; Wróć</b-link>
                </p>
                <legend><strong>Krok {{step}}.</strong> Ilość osób i termin rezerwacji</legend>
                <div class="row">
                    <div class="col-4">
                        <b-form-group label="*Liczba dorosłych">
                            <b-form-input required type="number" min="1" max="10" v-model="data.Adults"></b-form-input>
                        </b-form-group>
                    </div>
                    <div class="col-4">
                        <b-form-group label="*Liczba dzieci">
                            <b-form-input required type="number" min="0" max="10" v-model="data.Kids"></b-form-input>
                        </b-form-group>
                    </div>
                    <div class="col-4">
                        <b-form-group label="*Liczba zwierząt">
                            <b-form-input required type="number" min="0" max="10" v-model="data.Animals"></b-form-input>
                        </b-form-group>
                    </div>
                    <div class="col-6">
                         <b-form-group label="*Data rozpoczęcia pobytu">
                            <b-form-input required type="date" v-model="data.DateFrom"></b-form-input>
                        </b-form-group>
                    </div>
                    <div class="col-6">
                        <b-form-group label="*Data zakończenia pobytu">
                            <b-form-input required type="date" v-model="data.DateTo"></b-form-input>
                        </b-form-group>
                    </div>
                </div>
                <p class="text-right">
                    <b-button variant="outline-success" @click="fetchHouses" :disabled="!step1Valid">Nastepny krok</b-button>
                </p>
            </div>
            <div v-if="step == 2">
                
                <legend><strong>Krok {{step}}.</strong> Wybierz dostępny domek</legend>
                <p class="lead">Wyświetlane są jedynie domki spełniające podane kryteria: <br>
                    <b-badge variant="secondary">Ilość osób: <strong>{{ maxPersonCount }}</strong></b-badge> 
                    <b-badge variant="secondary">Data od: <strong>{{ data.DateFrom }}</strong></b-badge> 
                    <b-badge variant="secondary">Data do: <strong>{{ data.DateTo }}</strong></b-badge>
                </p>
                <b-alert variant="warning" :show="!housesCount"><font-awesome-icon icon="exclamation-circle"></font-awesome-icon> Brak domków spełniających podane kryteria.</b-alert>
                <b-table striped hover :busy="loading" :items="houses" :fields="fields" v-if="housesCount">
                    <div slot="table-busy" class="text-center text-info my-2">
                        <b-spinner class="align-middle"></b-spinner>
                        <strong> Ładowanie...</strong>
                    </div>
                    <template slot="price" slot-scope="data">
                        {{data.item.price}} zł
                    </template>
                    <template slot="show_details" slot-scope="house">
                        <b-button-group>
                        <b-button @click="function(){ data.houseId = house.item.houseId; choosenHouse = house.item; fetchClients(); }" variant="success" size="sm" title="Wybierz">
                            Wybierz
                        </b-button>
                        </b-button-group>
                    </template>
                    </b-table>
                <p class="text-right d-flex justify-content-between align-items-center">
                    <b-button @click="step -= 1">Poprzedni krok</b-button>
                </p>
            </div>
            <div v-if="step == 3">
                <legend><strong>Krok {{step}}.</strong> Uzupełnij dane gościa</legend>
                <b-form-group label="*Wybierz gościa z listy" label-for="maxPersonCount" v-if="!newClient">
                    <b-form-select required v-model="data.clientId" :options="clients"></b-form-select>
                </b-form-group>
                <p>
                    <b-form-checkbox v-model="newClient">
                        Nowy klient (dodaj)
                    </b-form-checkbox>
                </p>
                <div v-if="newClient">
                    <b-form @submit="onClientFormSubmit">
                        <b-form-group label="Imię">
                            <b-form-input v-model="newClientObject.Name" type="text" required placeholder="Wpisz imię"></b-form-input>
                        </b-form-group>
                        <b-form-group label="Nazwisko">
                            <b-form-input v-model="newClientObject.Lastname" type="text" required placeholder="Wpisz nazwisko"></b-form-input>
                        </b-form-group>
                        <b-form-group label="Adres e-mail">
                            <b-form-input v-model="newClientObject.Email" type="email" required placeholder="Wpisz adres e-mail"></b-form-input>
                        </b-form-group>
                        <b-form-group label="Telefon">
                            <b-form-input v-model="newClientObject.Phone" type="text" required placeholder="Wpisz telefon"></b-form-input>
                        </b-form-group>
                    </b-form>
                </div>
                
                <p class="text-right d-flex justify-content-between align-items-center">
                    <b-button @click="step -= 1">Poprzedni krok</b-button>
                    <b-button variant="outline-success" :disabled="!newClientValid" v-if="newClient" @click="onClientFormSubmit">Nastepny krok</b-button>
                    <b-button variant="outline-success" :disabled="data.clientId == undefined" v-if="!newClient" @click="step++">Nastepny krok</b-button>
                </p>
            </div>
            <div v-if="step == 4">
                <legend><strong>Krok {{step}}.</strong> Płatność i rezerwacja</legend>
                <b-form-checkbox v-model="data.AdvancePaid">
                        Zaliczka wpłacona
                </b-form-checkbox>
                <b-form-checkbox v-model="data.isPaid">
                        Całość zobowiązania wpłacona
                </b-form-checkbox>
                <hr>
                <b-card class="lead" border-variant="info"
                        header="Podsumowanie"
                        header-bg-variant="info"
                        header-text-variant="white">
                        <b-card-text>
                            <p>Ilość osób: <strong>{{maxPersonCount}}</strong></p>
                            <p>Ilość dni: <strong>{{dateDaysCount}}</strong> <b-badge variant="default"><small>od {{data.DateFrom}} do {{data.DateTo}}</small></b-badge></p>
                            <p>Kwota: <strong>{{choosenHouse.price * dateDaysCount}} zł</strong></p>
                            <p>
                                Zaliczka wpłacona: 
                                <strong v-if="data.AdvancePaid" class="text-success">Tak</strong>
                                <strong v-else class="text-danger">Nie</strong>
                            </p>
                            <p>
                                Całość zobowiązania opłacona: 
                                <strong v-if="data.isPaid" class="text-success">Tak</strong>
                                <strong v-else class="text-danger">Nie</strong>
                            </p>
                        </b-card-text>
                </b-card>
                <hr>
                <p class="text-right d-flex justify-content-between align-items-center">
                    <b-button @click="step -= 1">Poprzedni krok</b-button>
                    
                    <b-button variant="outline-success" size="lg" :disabled="data.clientId == undefined" @click="onSubmit">Zarezerwuj</b-button>
                </p>
            </div>
        </div>
    </div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import HomeService from '../../services/home.service';
import BookingResponse from '../../models/booking.model';
import HouseResponse from '../../models/house.model';
import Service from '../../services/service';
import ClientResponse from '../../models/client.model';

@Component
export default class BookingAdd extends Vue {
    public loading = true;
    public name = 'Rezerwacje - Dodaj';
    public homeService = new HomeService();
    public service = new Service();
    public data: BookingResponse = {} as BookingResponse;
    public houses: HouseResponse[] = [];
    public clients = [] as object[];
    public choosenClient: ClientResponse = {} as ClientResponse;
    public choosenHouse: HouseResponse = {} as HouseResponse;
    public newClient = false;
    public newClientObject = {} as ClientResponse;
    public fields: {} = {
      title: {
        label: 'Nazwa',
        sortable: true,
      },
      maxPersonCount: {
        label: 'Ilość miejsc',
        sortable: true,
      },
      rooms: {
        label: 'Pokoje',
        sortable: true,
      },
      bathrooms: {
        label: 'Łazienki',
        sortable: true,
      },
      floors: {
        label: 'Piętra',
        sortable: true,
      },
      price: {
        label: 'Koszt (doba)',
        sortable: true,
      },
      show_details: {
        label: '',
        sortable: false,
      },
    };
    public step = 1;

    public constructor() {
        super();
        this.data.Adults = '1';
        this.data.Kids = '0';
        this.data.Animals = '0';
        this.data.AdvancePaid = false;
        this.data.IsPaid = false;
    }

    public get maxPersonCount() {
        if(this.data.Kids != undefined && this.data.Adults != undefined) {
            return parseInt(this.data.Kids) + parseInt(this.data.Adults);
        }
        return 0;
    }

    public get housesCount() {
        return this.houses != undefined && this.houses.length > 0;
    }

    public get newClientValid() {
        const obj = this.newClientObject;
        const isUndefined = obj.Name == undefined || obj.Lastname == undefined || obj.Email == undefined || obj.Phone == undefined;
        if (!isUndefined) {
            return obj.Name != '' && obj.Lastname != '' && obj.Email != '' && obj.Phone != '';
        }
        return false;

    }

    public get step1Valid() {
        if (this.data.DateFrom != undefined && this.data.DateTo != undefined && this.data.Adults != undefined) {
            const d1 = new Date(this.data.DateFrom);
            const d2 = new Date(this.data.DateTo);
            return d1.getTime() < d2.getTime() && parseInt(this.data.Adults) >= 1;
        }

        return false;
    }

    public get dateDaysCount() {
        if (this.data.DateFrom != undefined && this.data.DateTo != undefined) {
            const d1 = new Date(this.data.DateFrom);
            const d2 = new Date(this.data.DateTo);
            const diffTime = Math.abs(d2.getTime() - d1.getTime());
            const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
            return diffDays;
        }
        return false;
    }

    public onClientFormSubmit() {
        this.loading = true;
        this.service.post('client', this.data)
            .then((response: any) => {
               this.data.ClientId = parseInt(response);
               this.step++;
            })
            .catch((error) => {
                console.log(error);
            })
            .finally(() => {
                this.loading = false;
            });
    }
    public getFormattedDate(d: string) {    
      var date = new Date(d);
      var str = date.getDate() + "-" + (date.getMonth() + 1) + "-" + date.getFullYear();
      return str;
    }
    private onSubmit() {
        this.loading = true;
        this.service.post(this.$route.matched[0].path, this.data)
            .then((response: any) => {
               this.$router.push(this.$route.matched[0].path);
            })
            .catch((error) => {
                console.log(error);
            })
            .finally(() => {
                this.loading = false;
            });
    }

    private fetchHouses() {
      this.loading = true;
      this.service.get('house')
            .then((response: any) => {
                console.log(response);
                const houses = response;
                this.houses = houses.filter((item: any) => item.maxPersonCount >= this.maxPersonCount);
            })
            .catch((error) => {
                console.log(error);
            })
            .finally(() => {
                this.step += 1;
                this.loading = false;
            });
    }

    private fetchClients() {
      this.loading = true;
      this.service.get('client')
            .then((response: any) => {
                const clients = response;
                clients.forEach((element: any) => {
                    this.clients.push({
                        text: element.name + ' ' + element.lastname,
                        value: element.clientId,
                    });
                });
            })
            .catch((error) => {
                console.log(error);
            })
            .finally(() => {
                this.step += 1;
                this.loading = false;
            });
    }
}
</script>
<style>
.badge {
    margin-right: 15px;
}
</style>

