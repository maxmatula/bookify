<template>
    <div>
        <h1>{{name}} (#{{data.equipmentId}})</h1>
        <p>
            <b-link :to="$route.matched[0].path">&laquo; Wróć</b-link>
        </p>
        <div class="row">
            <div class="col-sm-12 col-md-6">
                <b-form @submit="onSubmit">
                    <b-form-group label="Nazwa" label-for="modelName">
                        <b-form-input id="modelName" v-model="data.name" type="text" required placeholder="Wpisz nazwę"></b-form-input>
                    </b-form-group>
                    <b-form-group label="Wybierz domek">
                        <b-form-select required v-model="data.houseId" :options="houses"></b-form-select>
                    </b-form-group>
                    <b-button type="submit" variant="warning" :disabled="loading"><b-spinner class="align-middle" v-if="loading"></b-spinner> Aktualizuj</b-button>
                </b-form>
            </div>
        </div>
    </div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import Service from '../../services/service';
import EquipmentResponse from '../../models/equipment.model';
import ErrorFormatter from '../../error';
import HouseResponse from '../../models/house.model';

@Component
export default class EquipmentAdd extends Vue {
    public name = 'Wyposażenie - Dodaj';
    public data: EquipmentResponse = {} as EquipmentResponse;
    public houses = [] as object[];
    public loading = true;
    public fields = [];
    private service = new Service();

    public created() {
        this.loading = false;
        this.fetchHouses();
    }

    private fetchHouses() {
        this.service.get('house')
            .then((response: any) => {
                const houses = response;
                houses.forEach((element: any) => {
                    this.houses.push({
                        text: element.title,
                        value: element.houseId,
                    });
                });
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
        this.service.post(this.$route.matched[0].path, this.data)
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
}
</script>


