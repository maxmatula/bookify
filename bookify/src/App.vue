<template>
    <div :class="currentComponent">
        <header-component></header-component>
        <main>
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <router-view></router-view>
                    </div>
                </div>
            </div>
        </main>
    </div>
</template>

<script lang="ts">
import {Component, Vue} from 'vue-property-decorator';
import HeaderComponent from './components/Header.vue';
import {Http} from './http';
import Service from './services/service';
import EquipmentResponse from './models/equipment.model';

@Component({
    components: {HeaderComponent},
})
export default class App extends Vue {
    private service = new Service();
    get currentComponent() {
        return 'component-' + this.$route.name;
    }
    public created() {
        Http.interceptors.response.use(undefined, (err: any) => {
            return new Promise((resolve, reject) => {
                if (err.status === 401 && err.config && !err.config.__isRetryRequest) {
                    this.$store.dispatch('authLogout');
                }
                throw err;
            });
        });
    }
}
</script>