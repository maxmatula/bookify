<template>
    <div>
        <div class="col-12">
            <h1>{{name}}</h1>
        </div>
        <div class="col-sm-12 col-md-8">
                <p>
                    <b-link :to="$route.matched[0].path">&laquo; Wróć</b-link>
                </p>
                <legend>Ilość osób i termin rezerwacji</legend>
                <div class="row">
                    <div class="col-4">
                        <b-form-group label="*Liczba dorosłych">
                            <b-form-input required type="number" min="1" max="10" v-model="data.adults" @change="fetchHouses()"></b-form-input>
                        </b-form-group>
                    </div>
                    <div class="col-4">
                        <b-form-group label="*Liczba dzieci">
                            <b-form-input required type="number" min="0" max="10" v-model="data.kids" @change="fetchHouses()"></b-form-input>
                        </b-form-group>
                    </div>
                    <div class="col-4">
                        <b-form-group label="*Liczba zwierząt">
                            <b-form-input required type="number" min="0" max="10" v-model="data.animals"></b-form-input>
                        </b-form-group>
                    </div>
                    <div class="col-6">
                         <b-form-group label="*Data rozpoczęcia pobytu">
                            <b-form-input required type="date" v-model="data.dateFrom" @change="fetchHouses()"></b-form-input>
                        </b-form-group>
                    </div>
                    <div class="col-6">
                        <b-form-group label="*Data zakończenia pobytu">
                            <b-form-input required type="date" v-model="data.dateTo" @change="fetchHouses()"></b-form-input>
                        </b-form-group>
                    </div>
                </div>
                <hr>
                <legend>Wybierz dostępny domek</legend>
                <p class="lead">Wyświetlane są jedynie domki spełniające podane kryteria: <br>
                    <b-badge variant="secondary">Ilość osób: <strong>{{ maxPersonCount }}</strong></b-badge> 
                    <b-badge variant="secondary">Data od: <strong>{{ data.dateFrom }}</strong></b-badge> 
                    <b-badge variant="secondary">Data do: <strong>{{ data.dateTo }}</strong></b-badge>
                </p>
                <b-alert variant="warning" :show="!housesCount"><font-awesome-icon icon="exclamation-circle"></font-awesome-icon> Brak domków spełniających podane kryteria.</b-alert>
                <b-table striped hover :busy="loading" :items="houses" :fields="fields" v-if="housesCount" :tbody-tr-class="rowClass">
                    <div slot="table-busy" class="text-center text-info my-2">
                        <b-spinner class="align-middle"></b-spinner>
                        <strong> Ładowanie...</strong>
                    </div>
                    <template slot="price" slot-scope="data">
                        {{data.item.price}} zł
                    </template>
                    <template slot="show_details" slot-scope="house">
                        <b-button-group>
                        <b-button @click="function(){ data.houseId = house.item.houseId; choosenHouse = house.item; }" variant="success" size="sm" title="Wybierz">
                            Wybierz
                        </b-button>
                        </b-button-group>
                    </template>
                    </b-table>
                <legend>Uzupełnij dane gościa</legend>
                <b-form-group label="*Wybierz gościa z listy" label-for="maxPersonCount" v-if="!newClient">
                    <b-form-select required v-model="data.clientId" :options="clients"></b-form-select>
                </b-form-group>
                <hr>
                <legend>Płatność i rezerwacja</legend>
                <hr>
                <b-form-checkbox v-model="data.advancePaid">
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
                            <p>Ilość dni: <strong>{{dateDaysCount}}</strong> <b-badge variant="default"><small>od {{data.dateFrom}} do {{data.dateTo}}</small></b-badge></p>
                            <p>Kwota: <strong>{{choosenHouse.price * dateDaysCount}} zł</strong></p>
                            <p>
                                Zaliczka wpłacona: 
                                <strong v-if="data.advancePaid" class="text-success">Tak</strong>
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
                    <b-button variant="outline-warning" size="lg" :disabled="data.clientId === undefined" @click="onSubmit">Aktualizuj</b-button>
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
import ErrorFormatter from '../../error';

@Component
export default class BookingEdit extends Vue {
    public loading = true;
    public name = 'Rezerwacje - Edytuj';
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
        // this.data.adults = '1';
        // this.data.kids = '0';
        // this.data.animals = '0';
        // this.data.advancePaid = false;
        // this.data.isPaid = false;
    }

    public created() {
        this.fetch();
        this.fetchHouses();
        this.fetchClients();
    }

    public rowClass(item: HouseResponse) {
        return item.houseId === this.choosenHouse.houseId ? 'table-success' : '';
    }

    public get maxPersonCount() {
        if (this.data.kids !== undefined && this.data.adults !== undefined) {
            return parseInt(this.data.kids, 10) + parseInt(this.data.adults, 10);
        }
        return 0;
    }

    public get housesCount() {
        return this.houses !== undefined && this.houses.length > 0;
    }

    public get step1Valid() {
        if (this.data.dateFrom !== undefined && this.data.dateTo !== undefined && this.data.adults !== undefined) {
            const d1 = new Date(this.data.dateFrom);
            const d2 = new Date(this.data.dateTo);
            return d1.getTime() < d2.getTime() && parseInt(this.data.adults, 10) >= 1;
        }

        return false;
    }

    public get dateDaysCount() {
        if (this.data.dateFrom !== undefined && this.data.dateTo !== undefined) {
            const d1 = new Date(this.data.dateFrom);
            const d2 = new Date(this.data.dateTo);
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
               this.data.clientId = parseInt(response, 10);
               this.step++;
            })
            .catch((error) => {
                const alert = new ErrorFormatter(error);
            })
            .finally(() => {
                this.loading = false;
            });
    }
    public getFormattedDate(d: string) {
      const date = new Date(d);
      const year = date.getFullYear();
      let month = (date.getMonth() + 1).toString();
      if (parseInt(month, 10) < 10) { month = '0' + month; }
      let day = date.getDate().toString();
      if (parseInt(day, 10) < 10) { day = '0' + day; }
      const str = year + '-' + month + '-' + day;
      return str;
    }

    private fetch() {
        // model
        this.service.get(this.$route.path)
            .then((response: any) => {
                if (response.dateFrom !== undefined && response.dateTo !== undefined) {
                    response.dateFrom = this.getFormattedDate(response.dateFrom);
                    response.dateTo = this.getFormattedDate(response.dateTo);
                }
                this.data = response;
                this.choosenHouse = response.house;
            })
            .catch((error) => {
                const alert = new ErrorFormatter(error);
            })
            .finally(() => {
                this.loading = false;
            });

    }

    private onSubmit() {
        this.loading = true;
        this.service.post(this.$route.matched[0].path + '/edit', this.data)
            .then((response: any) => {
               this.$router.push(this.$route.matched[0].path);
            })
            .catch((error) => {
                const alert = new ErrorFormatter(error);
            })
            .finally(() => {
                this.loading = false;
            });
    }

    private fetchHouses() {
      this.loading = true;
      this.service.get('house')
            .then((response: any) => {
                const houses = response;
                this.houses = houses.filter((item: any) => item.maxPersonCount >= this.maxPersonCount);
            })
            .catch((error) => {
                const alert = new ErrorFormatter(error);
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
                const alert = new ErrorFormatter(error);
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

