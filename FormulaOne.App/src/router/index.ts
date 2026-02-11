import { createRouter, createWebHistory } from 'vue-router'
import DriversView from '../views/DriversView.vue'
import TeamsView from '../views/TeamsView.vue'
import MapView from '../views/MapView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/', redirect: '/drivers' },
    { path: '/drivers', component: DriversView },
    { path: '/teams', component: TeamsView },
    { path: '/map', component: MapView }
  ]
})

export default router