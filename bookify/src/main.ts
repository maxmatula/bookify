import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';

import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import '@/scss/style.scss';

import { library } from '@fortawesome/fontawesome-svg-core';
import { faTrash, faPencilAlt } from '@fortawesome/free-solid-svg-icons';
// import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

library.add(faTrash);
library.add(faPencilAlt);

import BootstrapVue from 'bootstrap-vue';

Vue.config.productionTip = false;
Vue.use(BootstrapVue);
new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');


