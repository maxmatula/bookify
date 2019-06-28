<template>
    <div>
        <router-view></router-view>
    </div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import Service from '@/services/service';
import EquipmentResponse from '@/models/equipment.model';

@Component({
    name: 'Domki',
})
export default class Booking extends Vue {

}
</script>
