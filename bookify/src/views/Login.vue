<template>
    <div class="container">
        <div class="row">
            <div class="col-md-6 col-sm-6 m-auto">
                <h1>Logowanie</h1>
                <hr>
                <b-form @submit="onSubmit">
                    <b-form-group label="Twój login" label-for="modelLogin" description="Identyfikator logowania">
                        <b-form-input id="modelLogin" v-model="login" type="text" required placeholder="Wpisz login"></b-form-input>
                    </b-form-group>
                    <b-form-group label="Twoje hasło" label-for="modelPassword" description="Hasło użytkownika">
                        <b-form-input id="modelPassword" v-model="password" type="password" required placeholder="Wpisz hasło"></b-form-input>
                    </b-form-group>
                    <p>
                        <b-button type="submit" variant="success" class="btn-block" size="lg">
                            Zaloguj się
                        </b-button>
                    </p>
                </b-form>
                <code>
                    <strong>KONTO TESTOWE</strong><br>
                    Login: admin<br>
                    Hasło: admin<br>
                </code>
            </div>
        </div>
    </div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class Login extends Vue {
    public login: string = '';
    public password: string = '';
    public onSubmit() {
        const {login, password} = this;
        this.$store.dispatch('authRequest', { login, password }).then(() => {
            this.$router.push('/');
        });
    }
}
</script>
