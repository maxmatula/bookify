import Vue from 'vue';
import Vuex from 'vuex';

import { Http } from './http';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    appName: 'Bookify',
    apiUrl: 'http://localhost:8000/',
    token: localStorage.getItem('user-token') || '',
    status: '',
  },
  mutations: {
    AUTH_REQUEST(state) {
      state.status = 'loading';
    },
    AUTH_SUCCESS(state, token) {
      state.status = 'success';
      state.token = token;
    },
    AUTH_ERROR(state) {
      state.status = 'error';
    },
    AUTH_LOGOUT(state) {
      state.token = '';
      state.status = '';
    },
  },
  actions: {
    authRequest(context, user) {
      return new Promise((resolve, reject) => {
        context.commit('AUTH_REQUEST');
        Http({url: 'api/identity/authenticate', data: user, method: 'post',
          headers: {'Content-Type': 'application/x-www-form-urlencoded' },
        })
          .then((resp: any) => {
            const token = resp.data.token;
            localStorage.setItem('user-token', token);
            // Axios.defaults.headers.common['Authorization'] = token;
            context.commit('AUTH_SUCCESS', token);
            resolve(resp);
          })
          .catch((err: any) => {
            alert(err.response.data.message);
            context.commit('AUTH_ERROR', err);
            localStorage.removeItem('user-token');
            reject(err);
          });
      });
    },
    authLogout(context) {
      return new Promise((resolve, reject) => {
        context.commit('AUTH_LOGOUT');
        localStorage.removeItem('user-token');
        // delete Axios.defaults.headers.common['Authorization']
        resolve();
      });
    },
  },
  getters: {
    isAuthenticated: (state) => !!state.token,
    authStatus: (state) => state.status,
  },
});
