<template>

		<div>
				<header class="main-header">
				<b-navbar toggleable="md">
						<div class="container">
								<div class="row w-100">
										<b-navbar-brand to="/" class="text-uppercase">
												{{name}}
										</b-navbar-brand>

										<b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

										<b-collapse id="nav-collapse" is-nav class="justify-content-end">
												<b-navbar-nav class="mr-0">
														<b-nav-item to="/">Home</b-nav-item>
														<b-nav-item to="/client">Lokale</b-nav-item>
														<b-nav-item to="/client">Rezerwacje</b-nav-item>
														<b-nav-item class="outline" to="/login" v-if="!$store.getters.isAuthenticated">Zaloguj się</b-nav-item>
														<b-nav-item @click="logout()" v-if="$store.getters.isAuthenticated">Wyloguj się</b-nav-item>

												</b-navbar-nav>
										</b-collapse>
								</div>
						</div>
				</b-navbar>
		</header>

				<div class="bgImage"></div>
				<svg class="blobCont">
						<defs>
								<filter id="gooey" height="130%">
										<feGaussianBlur in="SourceGraphic" stdDeviation="15" result="blur" />
										<feColorMatrix in="blur" mode="matrix" values="1 0 0 0 0  0 1 0 0 0  0 0 1 0 0  0 0 0 18 -7" result="goo" />
								</filter>
						</defs>
						<g style="filter: url(#gooey)">
								<circle class="blob" cx="10%" cy="10%" r="80" fill="#302579" />
								<circle class="blob" cx="50%" cy="10%" r="40" fill="#302579" />
								<circle class="blob" cx="17%" cy="15%" r="70" fill="#302579" />
								<circle class="blob" cx="90%" cy="20%" r="120" fill="white" />
								<circle class="blob" cx="30%" cy="25%" r="30" fill="#302579" />
								<circle class="blob" cx="50%" cy="60%" r="80" fill="#302579" />
								<circle class="blob" cx="70%" cy="90%" r="10" fill="#302579" />
								<circle class="blob" cx="90%" cy="60%" r="90" fill="#302579" />
								<circle class="blob" cx="30%" cy="90%" r="80" fill="#302579" />
								<circle class="blob" cx="10%" cy="10%" r="80" fill="#302579" />
								<circle class="blob" cx="50%" cy="10%" r="20" fill="#302579" />
								<circle class="blob" cx="17%" cy="15%" r="70" fill="#302579" />
								<circle class="blob" cx="40%" cy="20%" r="120" fill="#302579" />
								<circle class="blob" cx="30%" cy="25%" r="30" fill="#302579" />
								<circle class="blob" cx="80%" cy="60%" r="80" fill="#302579" />
								<circle class="blob" cx="17%" cy="10%" r="100" fill="#302579" />
								<circle class="blob" cx="40%" cy="60%" r="90" fill="#302579" />
								<circle class="blob" cx="10%" cy="50%" r="80" fill="#302579" />
						</g>
						<linearGradient id="hero-gradient" x2="1" y2="1">
								<stop offset="0%" stop-color="#302579" />
								<stop offset="100%" stop-color="#52d6d3" />
						</linearGradient>
				</svg>
		</div>
</template>

<script lang="ts">
    import {Component, Vue} from 'vue-property-decorator';

    @Component
    export default class Header extends Vue {
        get name() {
            return this.$store.state.appName;
        }
        public logout(): void {
            // LOGOUT
        }

    }
</script>