<template>
    <div>
        <router-view></router-view>
    </div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import Service from '../services/service';
import EquipmentResponse from '../models/equipment.model';

@Component({
    name: 'Klienci',
})
export default class Client extends Vue {

}
</script>
