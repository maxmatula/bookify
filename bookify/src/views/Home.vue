<template>
    <div class="hero-content text-center">
      <h1>System zarządzania rezerwacjami</h1>
      <p>Internetowy system zarządzania ośrodkiem wypoczynkowym oraz rezerwacjami.</p>
      <b-button variant="hero" size="lg" to="/login">Zaloguj się</b-button>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class Home extends Vue {}
</script>