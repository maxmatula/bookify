import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import ErrorFormatter from './error';

import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import '@/scss/style.scss';

import { library } from '@fortawesome/fontawesome-svg-core';
import { faCalendar, faHome, faUsers, faCogs, faExclamationCircle } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

Vue.component('font-awesome-icon', FontAwesomeIcon);

library.add(faCalendar);
library.add(faHome);
library.add(faUsers);
library.add(faCogs);
library.add(faExclamationCircle);

import BootstrapVue from 'bootstrap-vue';

Vue.config.productionTip = false;
Vue.use(BootstrapVue);

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');


