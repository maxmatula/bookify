import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import Login from './views/Login.vue';
import store from './store';
import Equipment from './views/Equipment.vue';
import EquipmentList from './components/equipment/EquipmentList.vue';
import EquipmentEdit from './components/equipment/EquipmentEdit.vue';
import EquipmentAdd from './components/equipment/EquipmentAdd.vue';

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
}

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
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ './views/About.vue'),
    },
  ],
});
