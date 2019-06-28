import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import Login from './views/Login.vue';
import store from './store';
import Equipment from './views/Equipment.vue';
import EquipmentList from './components/equipment/EquipmentList.vue';
import EquipmentEdit from './components/equipment/EquipmentEdit.vue';
import EquipmentAdd from './components/equipment/EquipmentAdd.vue';
import House from './views/House.vue';
import HouseList from './components/house/HouseList.vue';
import HouseAdd from './components/house/HouseAdd.vue';
import HouseEdit from './components/house/HouseEdit.vue';
import Client from './views/Client.vue';
import ClientList from './components/client/ClientList.vue';
import ClientAdd from './components/client/ClientAdd.vue';
import ClientEdit from './components/client/ClientEdit.vue';
import Booking from './views/Booking.vue';
import BookingList from './components/booking/BookingList.vue';
import BookingAdd from './components/booking/BookingAdd.vue';
import BookingEdit from './components/booking/BookingEdit.vue';

Vue.use(Router);

const ifNotAuthenticated = (to: any, from: any, next: any) => {
  if (!store.getters.isAuthenticated) {
    next();
    return;
  }
  next('/');
};

const ifAuthenticated = (to: any, from: any, next: any) => {
  if (store.getters.isAuthenticated) {
    next();
    return;
  }
  next('/login');
};

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
    },
    {
      path: '/login',
      component: Login,
    },
    {
      path: '/equipment',
      component: Equipment,
      beforeEnter: ifAuthenticated,
      children: [
        {
          path: '',
          component: EquipmentList,
        },
        {
          path: 'add',
          component: EquipmentAdd,
        },
        {
          name: 'equipment/edit',
          path: ':id',
          component: EquipmentEdit,
        },
      ],
    },
    {
      path: '/house',
      component: House,
      beforeEnter: ifAuthenticated,
      children: [
        {
          path: '',
          component: HouseList,
        },
        {
          path: 'add',
          component: HouseAdd,
        },
        {
          name: 'house/edit',
          path: ':id',
          component: HouseEdit,
        },
      ],
    },
    {
      path: '/client',
      component: Client,
      beforeEnter: ifAuthenticated,
      children: [
        {
          path: '',
          component: ClientList,
        },
        {
          path: 'add',
          component: ClientAdd,
        },
        {
          name: 'client/edit',
          path: ':id',
          component: ClientEdit,
        },
      ],
    },
    {
      path: '/booking',
      component: Booking,
      beforeEnter: ifAuthenticated,
      children: [
        {
          path: '',
          component: BookingList,
        },
        {
          path: 'add',
          component: BookingAdd,
        },
        {
          name: 'booking/edit',
          path: ':id',
          component: BookingEdit,
        },
      ],
    },
  ],
});
