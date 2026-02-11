<template>
  <div>
    <div v-if="successMessage" class="success-toast">{{ successMessage }}</div>

    <div class="toolbar">
      <h1>Drivers</h1>
      <div class="toolbar-actions">
        <input
          v-model="search"
          type="text"
          placeholder="Search drivers..."
          @input="fetchDrivers"
        />
        <button @click="openCreateForm">+ Add Driver</button>
      </div>
    </div>

    <div v-if="error" class="error">{{ error }}</div>
    <div v-if="loading">Loading...</div>

    <table v-else>
      <thead>
        <tr>
          <th @click="setSort('number')">#  {{ sortBy === 'number' ? '▲' : '' }}</th>
          <th @click="setSort('name')">Name {{ sortBy === 'name' ? '▲' : '' }}</th>
          <th>Nationality</th>
          <th @click="setSort('team')">Team {{ sortBy === 'team' ? '▲' : '' }}</th>
          <th @click="setSort('points')">Points {{ sortBy === 'points' ? '▼' : '' }}</th>
          <th>Active</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="driver in drivers" :key="driver.id">
          <td>{{ driver.number }}</td>
          <td>{{ driver.fullName }}</td>
          <td>{{ driver.nationality }}</td>
          <td>{{ driver.teamName }}</td>
          <td>{{ driver.points }}</td>
          <td>{{ driver.isActive ? 'Y' : 'N' }}</td>
          <td>
            <button @click="openEditForm(driver)">Edit</button>
            <button @click="deleteDriver(driver.id)" class="btn-danger">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Modal -->
    <div v-if="showForm" class="modal-overlay">
      <div class="modal">
        <h2>{{ editingDriver ? 'Edit Driver' : 'Add Driver' }}</h2>
        <div v-if="formError" class="error">{{ formError }}</div>
        <form @submit.prevent="submitForm">
          <label>Full Name
            <input v-model="form.fullName" required />
          </label>
          <label>Number
            <input v-model.number="form.number" type="number" min="1" max="99" required />
          </label>
          <label>Nationality
            <input v-model="form.nationality" required />
          </label>
          <label>Team
            <select v-model.number="form.teamId" required>
              <option v-for="team in teams" :key="team.id" :value="team.id">
                {{ team.name }}
              </option>
            </select>
          </label>
          <label>Points
            <input v-model.number="form.points" type="number" min="0" required />
          </label>
          <label class="checkbox-label">
            <input v-model="form.isActive" type="checkbox" />
            Active
          </label>
          <div class="form-actions">
            <button type="submit">{{ editingDriver ? 'Save' : 'Create' }}</button>
            <button type="button" @click="closeForm">Cancel</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { getDrivers, createDriver, updateDriver, deleteDriver as apiDeleteDriver, getTeams } from '../services/api'

interface Driver {
  id: number
  fullName: string
  number: number
  nationality: string
  teamId: number
  teamName: string
  points: number
  isActive: boolean
}

interface Team {
  id: number
  name: string
}

interface DriverForm {
  fullName: string
  number: number | null
  nationality: string
  teamId: number | null
  points: number
  isActive: boolean
}

const drivers = ref<Driver[]>([])
const teams = ref<Team[]>([])
const search = ref('')
const sortBy = ref('number')
const loading = ref(false)
const error = ref('')
const formError = ref('')
const showForm = ref(false)
const editingDriver = ref<Driver | null>(null)
const successMessage = ref('')

const form = ref<DriverForm>({
  fullName: '',
  number: null,
  nationality: '',
  teamId: null,
  points: 0,
  isActive: true
})

const fetchDrivers = async () => {
  loading.value = true
  error.value = ''
  try {
    const response = await getDrivers(search.value, sortBy.value)
    drivers.value = response.data
  } catch {
    error.value = 'Failed to load drivers.'
  } finally {
    loading.value = false
  }
}

const fetchTeams = async () => {
  const response = await getTeams()
  teams.value = response.data
}

const setSort = (column: string) => {
  sortBy.value = column
  fetchDrivers()
}

const openCreateForm = () => {
  editingDriver.value = null
  form.value = { fullName: '', number: null, nationality: '', teamId: null, points: 0, isActive: true }
  formError.value = ''
  showForm.value = true
}

const openEditForm = (driver: Driver) => {
  editingDriver.value = driver
  form.value = {
    fullName: driver.fullName,
    number: driver.number,
    nationality: driver.nationality,
    teamId: driver.teamId,
    points: driver.points,
    isActive: driver.isActive
  }
  formError.value = ''
  showForm.value = true
}

const closeForm = () => {
  showForm.value = false
}

const submitForm = async () => {
  formError.value = ''
  try {
    if (editingDriver.value) {
      await updateDriver(editingDriver.value.id, form.value)
      successMessage.value = 'Driver updated successfully'
    } else {
      await createDriver(form.value)
      successMessage.value = 'Driver created successfully'
    }
    showForm.value = false
    fetchDrivers()
    setTimeout(() => successMessage.value = '', 3000) // clear after 3 seconds
  } catch (err: any) {
    formError.value = err.response?.data?.title
      || err.response?.data?.message
      || err.response?.data
      || 'An error occurred.'
  }
}

const deleteDriver = async (id: number) => {
  if (!confirm('Are you sure you want to delete this driver?')) return
  try {
    await apiDeleteDriver(id)
    successMessage.value = 'Driver deleted successfully'
    fetchDrivers()
    setTimeout(() => successMessage.value = '', 3000)
  } catch {
    error.value = 'Failed to delete driver.'
  }
}

onMounted(() => {
  fetchDrivers()
  fetchTeams()
})
</script>

<style scoped>
.toolbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1rem;
}

.toolbar-actions {
  display: flex;
  gap: 1rem;
}

input[type="text"] {
  padding: 0.5rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  width: 250px;
}

table {
  width: 100%;
  border-collapse: collapse;
  background: white;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 1px 3px rgba(0,0,0,0.1);
}

th, td {
  padding: 0.75rem 1rem;
  text-align: left;
  border-bottom: 1px solid #eee;
}

th {
  background-color: #1a1a1a;
  color: white;
  cursor: pointer;
  user-select: none;
}

th:hover {
  background-color: #333;
}

tr:hover {
  background-color: #f9f9f9;
}

button {
  padding: 0.4rem 0.8rem;
  margin-right: 0.25rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  background-color: #b40a04;
  color: white;
}

button:hover {
  opacity: 0.85;
}

.btn-danger {
  background-color: #666;
}

.modal-overlay {
  position: fixed;
  inset: 0;
  background: rgba(0,0,0,0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
}

.modal {
  background: white;
  padding: 2rem;
  border-radius: 8px;
  width: 400px;
  max-width: 90%;
}

.modal h2 {
  margin-bottom: 1rem;
}

form {
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
}

label {
  display: flex;
  flex-direction: column;
  gap: 0.25rem;
  font-size: 0.9rem;
  font-weight: bold;
}

label input, label select {
  padding: 0.5rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 1rem;
}

.checkbox-label {
  flex-direction: row;
  align-items: center;
  gap: 0.5rem;
}

.form-actions {
  display: flex;
  gap: 0.5rem;
  margin-top: 0.5rem;
}

.error {
  color: #e10600;
  margin-bottom: 1rem;
}

.success-toast {
  position: fixed;
  top: 2rem;
  background-color: #4caf50;
  color: white;
  padding: 1rem 1.5rem;
  border-radius: 4px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.2);
  z-index: 2000;
}
</style>