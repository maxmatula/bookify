<template>
    <div>
        <h1>{{name}}</h1>
        <p>
            <b-button variant="success" :to="($route.path).replace('/','') + '/add'">Utwórz nowy</b-button>
          </p>
        <b-table striped hover :busy="loading" :items="data" :fields="fields">
          <div slot="table-busy" class="text-center text-info my-2">
            <b-spinner class="align-middle"></b-spinner>
            <strong> Ładowanie...</strong>
          </div>
          <template slot="houseId" slot-scope="data">
            {{data.item.house.title}}
          </template>
          <template slot="show_details" slot-scope="data">
            <b-button-group>
              <b-button title="Edytuj" :to="{ name: ($route.path).replace('/','') + '/edit', params: { id: data.item.equipmentId }}" variant="warning" size="sm">
                Edytuj
              </b-button>
              <b-button @click="onDelete(data.item.equipmentId)" variant="danger" size="sm" title="Usuń">
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
import EquipmentResponse from '../../models/equipment.model';
import ErrorFormatter from '../../error';

@Component
export default class EquipmentList extends Vue {
    public name =  'Ewidencja wyposażenia ośrodka';
    public data = [];
    public loading = true;

    public fields: {} = {
      equipmentId: {
        label: '#',
        sortable: true,
      },
      name: {
        label: 'Nazwa',
        sortable: false,
      },
      houseId: {
        label: 'Domek',
        sortable: false,
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


