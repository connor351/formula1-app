<template>
  <div>
    <h1>F1 Circuit Map</h1>
    <br>
    <h4>Click on a marker for more information about a circuit</h4>
    <div v-if="error" class="error">{{ error }}</div>
    <div v-if="loading">Loading circuits...</div>
    <div v-else class="map-container">
      <div id="map"></div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import L from 'leaflet'
import { getCircuits } from '../services/api'

const loading = ref(true)
const error = ref('')

interface Circuit {
  id: number
  name: string
  country: string
  city: string
  lat: number
  lng: number
  roundNumber: number
}

const circuits = ref<Circuit[]>([])

onMounted(async () => {
  try {
    console.log('Fetching circuits...')
    const response = await getCircuits()
    console.log('Response:', response)
    circuits.value = response.data
    console.log('Circuits loaded:', circuits.value)
    
    loading.value = false

    await new Promise(resolve => setTimeout(resolve, 0))

    // Initialize the map centered on Europe
    const map = L.map('map').setView([45, 10], 3)

    // Add map tiles
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '© OpenStreetMap contributors'
    }).addTo(map)

    console.log('Map initialized')

    // Add markers
    circuits.value.forEach(circuit => {
    const marker = L.marker([circuit.lat, circuit.lng]).addTo(map)
  
    // Popup info
    marker.bindPopup(`
    <strong>${circuit.name}</strong><br>
    ${circuit.city}, ${circuit.country}<br>
    Round ${circuit.roundNumber}
  `)
})

console.log('Map initialized with', circuits.value.length, 'markers')
  } catch (err) {
    console.error('Error:', err)
    error.value = 'Failed to load circuits.'
    loading.value = false
  }
})
</script>

<style scoped>
.error {
  color: #e10600;
  margin-bottom: 1rem;
}

.map-container {
  background: white;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 1px 3px rgba(0,0,0,0.1);
  margin-top: 1rem;
}

#map {
  height: 800px;
  width: 100%;
}
</style>
