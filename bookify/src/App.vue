<template>
    <div :class="currentComponent">
        <header-component></header-component>
        <router-view></router-view>
    </div>
</template>

<script lang="ts">
    import {Component, Vue} from 'vue-property-decorator';
    import HeaderComponent from "./components/Header.vue";
    @Component({
        components: {HeaderComponent}
    })
    export default class App extends Vue
		{
			get currentComponent() {
		  	return 'component-' + this.$route.name;
			}
    }
</script>