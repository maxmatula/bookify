import Vue from 'vue';
import Vuex from 'vuex';

import { Http } from './http';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    appName: 'Bookify',
    apiUrl: 'http://localhost:5000/',
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
          if (user.login === 'admin' && user.password === 'admin') {
            localStorage.setItem('user-token', 'o28137v60239');
            context.commit('AUTH_SUCCESS', 'o28137v60239');
            resolve();
          }
          reject();
          context.commit('AUTH_ERROR', 'Błędne dane logowania');
      });
    },
    authLogout(context) {
      return new Promise((resolve, reject) => {
        context.commit('AUTH_LOGOUT');
        localStorage.removeItem('user-token');
        resolve();
      });
    },
  },
  getters: {
    isAuthenticated: (state) => !!state.token,
    authStatus: (state) => state.status,
  },
});
