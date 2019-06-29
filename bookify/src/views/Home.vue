<template>
    <div class="hero-content text-center">
      <div v-if="!$store.getters.isAuthenticated">
        <h1>System zarządzania rezerwacjami</h1>
        <p>Internetowy system zarządzania ośrodkiem wypoczynkowym oraz rezerwacjami.</p>
        <b-button variant="hero" size="lg" to="/login">Zaloguj się</b-button>
      </div>
      <div v-else class="hero-content-logged">
        <h1>Sprawdź terminy rezerwacji:</h1>
        <div class="row">
            <div class="col-3">
                <b-form-group label="*Liczba dorosłych">
                    <b-form-input required type="number" min="1" max="10" v-model="data.adults" @change="fetchHouses()"></b-form-input>
                </b-form-group>
            </div>
            <div class="col-3">
                <b-form-group label="*Liczba dzieci">
                    <b-form-input required type="number" min="0" max="10" v-model="data.kids" @change="fetchHouses()"></b-form-input>
                </b-form-group>
            </div>
            <div class="col-3">
                  <b-form-group label="*Data rozpoczęcia pobytu">
                    <b-form-input required type="date" v-model="data.dateFrom" @change="fetchHouses()"></b-form-input>
                </b-form-group>
            </div>
            <div class="col-3">
                <b-form-group label="*Data zakończenia pobytu">
                    <b-form-input required type="date" v-model="data.dateTo" @change="fetchHouses()"></b-form-input>
                </b-form-group>
            </div>
            <div class="col-12">
              <b-alert variant="info" :show="!formValid"><font-awesome-icon icon="exclamation-circle"></font-awesome-icon> Wypełnij wszystkie pola, aby zobaczyć dostępne domki.</b-alert>
            </div>
        </div>
        <div class="row" v-if="formValid">
          <b-alert variant="warning" :show="!housesCount"><font-awesome-icon icon="exclamation-circle"></font-awesome-icon> Brak domków spełniających podane kryteria.</b-alert>
          <b-table striped hover :busy="loading" :items="houses" :fields="fields" v-if="housesCount">
              <div slot="table-busy" class="text-center text-info my-2">
                  <b-spinner class="align-middle"></b-spinner>
                  <strong> Ładowanie...</strong>
              </div>
              <template slot="price" slot-scope="data">
                  {{data.item.price}} zł
              </template>
          </b-table>
          <hr>
          <p class="col-12 text-center">
            <b-button variant="primary" to="/booking">Przejdź do rezerwacji &raquo;</b-button>
          </p>
        </div>
      </div>
      
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import HomeService from '../services/home.service';
import BookingResponse from '../models/booking.model';
import HouseResponse from '../models/house.model';
import ErrorFormatter from '../error';

@Component
export default class Home extends Vue {
  public loading = true;
  public homeService = new HomeService();
  public data: BookingResponse = {} as BookingResponse;
  public houses: HouseResponse[] = [];
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

    constructor() {
      super();
      this.data.adults = '1';
      this.data.kids = '0';
    }

    public created() {
        this.fetchHouses();
    }

    public get datesValid() {
        if (this.data.dateFrom !== undefined && this.data.dateTo !== undefined && this.data.adults !== undefined) {
            const d1 = new Date(this.data.dateFrom);
            const d2 = new Date(this.data.dateTo);
            return d1.getTime() < d2.getTime() && parseInt(this.data.adults, 10) >= 1;
        }

        return false;
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

    public get formValid() {
      if(this.data.adults === undefined) return false;
      if(this.data.kids === undefined) return false;
      if(this.data.adults === '') return false;
      if(this.data.kids === '') return false;
      if(this.data.dateFrom === undefined) return false;
      if(this.data.dateTo === undefined) return false;
      if(this.data.dateFrom === '') return false;
      if(this.data.dateTo === '') return false;
      return true;
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

    private fetchHouses() {
      this.loading = true;
      this.homeService.get('house')
            .then((response: any) => {
                const houses = response;
                this.houses = houses.filter((item: any) => {
                    
                    if(item !== undefined){
                        if(item.bookings.length === 0 || item.bookings === undefined) {
                            return item.maxPersonCount >= this.maxPersonCount;
                        }
                        if(this.data.dateFrom !== undefined && this.data.dateTo !== undefined ){
                            const bookingDateFrom = new Date(this.getFormattedDate(this.data.dateFrom)).getTime();
                            const bookingDateTo = new Date(this.getFormattedDate(this.data.dateTo)).getTime();
                            if(bookingDateFrom > bookingDateTo) return false;
                            let available = true;
                            item.bookings.forEach((element: any) => {
                                const houseDateFrom = new Date(this.getFormattedDate(element.dateFrom)).getTime();
                                const houseDateTo = new Date(this.getFormattedDate(element.dateTo)).getTime();
                                if(bookingDateFrom === houseDateFrom
                                    || bookingDateTo === houseDateTo
                                    || (bookingDateTo < houseDateTo && bookingDateTo > houseDateFrom)
                                    || (bookingDateTo > houseDateTo && bookingDateFrom > houseDateFrom && bookingDateFrom < houseDateTo)
                                    ) available = false;
                            });
                            return item.maxPersonCount >= this.maxPersonCount && available;

                        }
                        return item.maxPersonCount >= this.maxPersonCount;
                    }
                    return false;
                });
            })
            .catch((error) => {
                const alert = new ErrorFormatter(error);
            })
            .finally(() => {
                this.loading = false;
            });
    }
}
</script>
<style lang="scss" scoped>
.hero-content-logged {
  background-color: #fefefe;
  color: #333;
  padding: 30px;
}
</style>
