<template>
    <div>
        <h1>{{name}} (#{{data.clientId}})</h1>
        <p>
            <b-link :to="$route.matched[0].path">&laquo; Wróć</b-link>
        </p>
        <div class="row">
            <div class="col-sm-12 col-md-6">
                <b-form @submit="onSubmit">
                    <b-form-group label="Imię">
                        <b-form-input v-model="data.name" type="text" required placeholder="Wpisz imię"></b-form-input>
                    </b-form-group>
                    <b-form-group label="Nazwisko">
                        <b-form-input v-model="data.lastname" type="text" required placeholder="Wpisz nazwisko"></b-form-input>
                    </b-form-group>
                    <b-form-group label="Adres e-mail">
                        <b-form-input v-model="data.email" type="email" required placeholder="Wpisz adres e-mail"></b-form-input>
                    </b-form-group>
                    <b-form-group label="Telefon">
                        <b-form-input v-model="data.phone" type="text" required placeholder="Wpisz telefon"></b-form-input>
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

@Component
export default class ClientEdit extends Vue {
    public name = 'Klienci - Edycja';
    public data = {};
    public loading = true;
    public fields = [];
    private service = new Service();

    public created() {
        this.fetch();
    }

    private fetch() {
        // model
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


    private onSubmit() {
        this.loading = true;
        this.service.post(this.$route.matched[0].path + '/edit', this.data)
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


