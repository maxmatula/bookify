<template>
    <div>
        <h1>{{$options.name}}</h1>
        <p>
            <b-button variant="success" :to="($route.path).replace('/','') + '/add'">Utwórz nowy</b-button>
          </p>
        <b-table striped hover :busy="loading" :items="data" :fields="fields">
          <div slot="table-busy" class="text-center text-info my-2">
            <b-spinner class="align-middle"></b-spinner>
            <strong> Ładowanie...</strong>
          </div>
          <template slot="bookingId" slot-scope="data">
            #{{data.item.bookingId}}
          </template>
          <template slot="dateFrom" slot-scope="data">
            {{getFormattedDate(data.item.dateFrom)}}
          </template>
          <template slot="dateTo" slot-scope="data">
            {{getFormattedDate(data.item.dateTo)}}
          </template>
          <template slot="advancePaid" slot-scope="data">
            {{getFormattedBool(data.item.advancePaid)}}
          </template>
          <template slot="isPaid" slot-scope="data">
            {{getFormattedBool(data.item.isPaid)}}
          </template>
          <template slot="bookingDate" slot-scope="data">
            {{getFormattedDate(data.item.bookingDate)}}
          </template>
          <template slot="price" slot-scope="data">
            {{data.item.price}} zł
          </template>
          <template slot="show_details" slot-scope="data">
            <b-button-group>
              <b-button title="Edytuj" :to="{ name: ($route.path).replace('/','') + '/edit', params: { id: data.item.bookingId }}" variant="warning" size="sm">
                Edytuj
              </b-button>
              <b-button @click="onDelete(data.item.bookingId)" variant="danger" size="sm" title="Usuń">
                Usuń
                </b-button>
            </b-button-group>
          </template>
        </b-table>
        <p class="small">Wyświetlono: {{data.length}}</p>
    </div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import Service from '../../services/service';
import BookingResponse from '../../models/booking.model';
import { AxiosError } from 'axios';
import ErrorFormatter from '../../error';

@Component({
  name: 'Rezerwacje',
})
export default class BookingList extends Vue {
    public data = [];
    public loading = true;
    public fields: {} = {
      bookingId: {
        label: 'Numer rez.',
        sortable: true,
      },
      dateFrom: {
        label: 'Pobyt od',
        sortable: true,
      },
      dateTo: {
        label: 'Pobyt do',
        sortable: true,
      },
      adults: {
        label: 'Dorośli',
        sortable: true,
      },
      kids: {
        label: 'Dzieci',
        sortable: true,
      },
      advancePaid: {
        label: 'Zaliczka',
        sortable: true,
      },
      isPaid: {
        label: 'Opłacono w całości',
        sortable: true,
      },
      bookingDate: {
        label: 'Data rezerwacji',
        sortable: true,
      },
      show_details: {
        label: '',
      },
    };
    private service = new Service();
    public created() {
        this.fetch();
    }

    public getFormattedDate(d: string) {
      const date = new Date(d);
      const str = date.getDate() + '-' + (date.getMonth() + 1) + '-' + date.getFullYear();
      return str;
    }
    public getFormattedBool(b: boolean) {
      if (b) { return 'Tak'; }
      return 'Nie';
    }

    private fetch() {
      this.loading = true;
      this.service.get(this.$route.path)
            .then((response: any) => {
                this.data = response;
            })
            .catch((error: AxiosError) => {
              const alert = new ErrorFormatter(error);
            })
            .finally(() => {
                this.loading = false;
            });
    }

    private onDelete(id: number) {
      const agree = confirm('Czy na pewno chcesz usunąć tą pozycję?');
      if (!agree) { return; }
      this.loading = true;
      this.service.delete(this.$route.path + '/' + id)
            .then((response: any) => {
                this.data = response;
            })
            .catch((error) => {
                const alert = new ErrorFormatter(error);
            })
            .finally(() => {
                this.fetch();
                this.loading = false;
            });
    }
}
</script>


