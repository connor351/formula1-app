<template>
  <nav>
    <div class="nav-brand">F1 Manager App</div>
    <RouterLink to="/drivers">Drivers</RouterLink>
    <RouterLink to="/teams">Teams</RouterLink>
    <RouterLink to="/map">Map</RouterLink>
  </nav>
  <main>
    <RouterView />
  </main>
</template>

<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
</script>

<style scoped>
nav {
  background-color: #b40a04;
  padding: 1rem 2rem;
  display: flex;
  gap: 2rem;
  align-items: center;
}

.nav-brand {
  color: white;
  font-weight: bold;
  font-size: 1.5rem;
  margin-right: 1rem;
}

nav a {
  color: white;
  text-decoration: none;
  font-weight: bold;
  font-size: 1rem;
}

nav a.router-link-active {
  text-decoration: underline;
}

main {
  padding: 2rem;
}
</style>