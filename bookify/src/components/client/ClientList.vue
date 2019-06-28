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
          <template slot="show_details" slot-scope="data">
            <b-button-group>
              <b-button title="Edytuj" :to="{ name: ($route.path).replace('/','') + '/edit', params: { id: data.item.clientId }}" variant="warning" size="sm">
                Edytuj
              </b-button>
              <b-button @click="onDelete(data.item.clientId)" variant="danger" size="sm" title="Usuń">
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
import ClientResponse from '../../models/client.model';

@Component({
  name: 'Klienci',
})
export default class ClientList extends Vue {
    public data = [];
    public loading = true;

    public fields: {} = {
      clientId: {
        label: '#',
        sortable: true,
      },
      name: {
        label: 'Imię',
        sortable: true,
      },
      lastname: {
        label: 'Nazwisko',
        sortable: true,
      },
      email: {
        label: 'Adres e-mail',
        sortable: true,
      },
      phone: {
        label: 'Telefon',
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

    private fetch() {
      this.loading = true;
      this.service.get(this.$route.path)
            .then((response: any) => {
                this.data = response;
            })
            .catch((error) => {
                console.log(error);
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
                console.log(error);
            })
            .finally(() => {
                this.fetch();
                this.loading = false;
            });
    }
}
</script>
<style lang="scss">
  tr{
    td, th{
      text-align: left !important;
      &:last-child {
      text-align: right !important;
    }
    }
  }
</style>


