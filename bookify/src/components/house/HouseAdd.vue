<template>
    <div>
        <h1>{{name}} (#{{data.houseId}})</h1>
        <p>
            <b-link :to="$route.matched[0].path">&laquo; Wróć</b-link>
        </p>
        <b-form @submit="onSubmit" class="row">
            <div class="col-sm-12 col-md-6">
                    <b-form-group label="Tytuł" label-for="title">
                    <b-form-input id="title" v-model="data.title" type="text" required placeholder="Wpisz tytuł"></b-form-input>
                </b-form-group>
                <b-form-group label="Nazwa" label-for="description">
                        <b-form-textarea id="description" v-model="data.description" placeholder="Opis" rows="3" max-rows="6" ></b-form-textarea>
                </b-form-group>
                <b-form-group label="Liczba pięter" label-for="floors">
                    <b-form-select v-model="data.floors" :options="service.floors"></b-form-select>
                </b-form-group>
                <b-form-group label="Liczba pokoi" label-for="rooms">
                     <b-form-select v-model="data.rooms" :options="service.rooms"></b-form-select>
                </b-form-group>
                <b-form-group label="Liczba łazienek" label-for="bathrooms">
                    <b-form-select v-model="data.bathrooms" :options="service.bathrooms"></b-form-select>
                </b-form-group>
                <b-form-group label="Cena za dobę" label-for="price">
                    <b-form-input id="price" v-model="data.price" type="number" min="1" max="99999999" step="0.10" required placeholder="Wpisz kwotę"></b-form-input>
                </b-form-group>
                <p>
                    <b-form-checkbox id="checkbox-1" v-model="customAddress">
                        Dodaj dane adresowe
                    </b-form-checkbox>
                </p>
                </div>
                
                <div class="col-sm-12 col-md-6">
                    <div v-if="customAddress">
                    <b-form-group label="Kraj">
                        <b-form-select v-model="data.country" :options="service.countries"></b-form-select>
                    </b-form-group>
                    <b-form-group label="Miasto">
                        <b-form-input v-model="data.city" type="text" placeholder="Wpisz miasto"></b-form-input>
                    </b-form-group>
                    <b-form-group label="Ulica">
                        <b-form-input v-model="data.street" type="text" placeholder="Wpisz ulicę"></b-form-input>
                    </b-form-group>
                    <b-form-group label="Numer domu">
                        <b-form-input v-model="data.houseNumber" type="text" placeholder="Wpisz numer domu"></b-form-input>
                    </b-form-group>
                    <b-form-group label="Numer mieszkania">
                        <b-form-input v-model="data.apartmentNumber" type="text" placeholder="Wpisz numer mieszkania"></b-form-input>
                    </b-form-group>
                    </div>
                </div>
            <div class="col-sm-12">
                 <b-button type="submit" variant="success" :disabled="loading"><b-spinner class="align-middle" v-if="loading"></b-spinner>Dodaj</b-button>
            </div>
        </b-form>
    </div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import Service from '@/services/home.service';
import HouseResponse from '../../models/house.model';

@Component
export default class HouseAdd extends Vue {
    public name = 'Domki - Dodaj';
    public data: HouseResponse = {} as HouseResponse;
    public loading = true;
    public fields = [];
    public countries = [
        { value: 'Polska', text: 'Polska' },
    ];
    public customAddress = false;
    private service = new Service();

    public created() {
        this.loading = false;
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
}
</script>


