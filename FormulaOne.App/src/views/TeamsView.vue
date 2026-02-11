<template>
  <div>
    <div v-if="successMessage" class="success-toast">{{ successMessage }}</div>

    <div class="toolbar">
      <h1>Teams</h1>
      <div class="toolbar-actions">
        <input
          v-model="search"
          type="text"
          placeholder="Search teams..."
          @input="fetchTeams"
        />
        <button @click="openCreateForm">+ Add Team</button>
      </div>
    </div>

    <div v-if="error" class="error">{{ error }}</div>
    <div v-if="loading">Loading...</div>

    <table v-else>
      <thead>
        <tr>
          <th>Name</th>
          <th>Team Principal</th>
          <th>Country</th>
          <th>City</th>
          <th>Drivers</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="team in teams" :key="team.id">
          <td>{{ team.name }}</td>
          <td>{{ team.teamPrincipal }}</td>
          <td>{{ team.country }}</td>
          <td>{{ team.city }}</td>
          <td>{{ team.driverCount }}</td>
          <td>
            <button @click="openEditForm(team)">Edit</button>
            <button @click="handleDelete(team)" class="btn-danger">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Delete conflict modal -->
    <div v-if="showConflict" class="modal-overlay">
      <div class="modal">
        <h2>Cannot Delete Team</h2>
        <p>{{ conflictMessage }}</p>
        <table class="conflict-table">
          <thead>
            <tr>
              <th>#</th>
              <th>Name</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="driver in conflictDrivers" :key="driver.id">
              <td>{{ driver.number }}</td>
              <td>{{ driver.fullName }}</td>
            </tr>
          </tbody>
        </table>
        <div class="form-actions">
          <button @click="showConflict = false">Close</button>
        </div>
      </div>
    </div>

    <!-- Create/Edit modal -->
    <div v-if="showForm" class="modal-overlay">
      <div class="modal">
        <h2>{{ editingTeam ? 'Edit Team' : 'Add Team' }}</h2>
        <div v-if="formError" class="error">{{ formError }}</div>
        <form @submit.prevent="submitForm">
          <label>Name
            <input v-model="form.name" required />
          </label>
          <label>Team Principal
            <input v-model="form.teamPrincipal" required />
          </label>
          <label>Country
            <input v-model="form.country" required />
          </label>
          <label>City
            <input v-model="form.city" required />
          </label>
          <div class="form-actions">
            <button type="submit">{{ editingTeam ? 'Save' : 'Create' }}</button>
            <button type="button" @click="closeForm">Cancel</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { getTeams, createTeam, updateTeam, deleteTeam } from '../services/api'

interface Team {
  id: number
  name: string
  teamPrincipal: string
  country: string
  city: string
  driverCount: number
}

interface TeamForm {
  name: string
  teamPrincipal: string
  country: string
  city: string
}

interface ConflictDriver {
  id: number
  fullName: string
  number: number
}

const teams = ref<Team[]>([])
const search = ref('')
const loading = ref(false)
const error = ref('')
const formError = ref('')
const successMessage = ref('')
const showForm = ref(false)
const editingTeam = ref<Team | null>(null)
const showConflict = ref(false)
const conflictMessage = ref('')
const conflictDrivers = ref<ConflictDriver[]>([])

const form = ref<TeamForm>({
  name: '',
  teamPrincipal: '',
  country: '',
  city: ''
})

const fetchTeams = async () => {
  loading.value = true
  error.value = ''
  try {
    const response = await getTeams(search.value)
    teams.value = response.data
  } catch {
    error.value = 'Failed to load teams.'
  } finally {
    loading.value = false
  }
}

const openCreateForm = () => {
  editingTeam.value = null
  form.value = { name: '', teamPrincipal: '', country: '', city: '' }
  formError.value = ''
  showForm.value = true
}

const openEditForm = (team: Team) => {
  editingTeam.value = team
  form.value = {
    name: team.name,
    teamPrincipal: team.teamPrincipal,
    country: team.country,
    city: team.city
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
    if (editingTeam.value) {
      await updateTeam(editingTeam.value.id, form.value)
      successMessage.value = 'Team updated successfully'
    } else {
      await createTeam(form.value)
      successMessage.value = 'Team created successfully'
    }
    showForm.value = false
    fetchTeams()
    setTimeout(() => successMessage.value = '', 3000)
  } catch (err: any) {
    formError.value = err.response?.data?.title
      || err.response?.data?.message
      || err.response?.data
      || 'An error occurred.'
  }
}

const handleDelete = async (team: Team) => {
  if (!confirm(`Are you sure you want to delete ${team.name}?`)) return
  try {
    await deleteTeam(team.id)
    successMessage.value = 'Team deleted successfully'
    fetchTeams()
    setTimeout(() => successMessage.value = '', 3000)
  } catch (err: any) {
    if (err.response?.status === 409) {
      conflictMessage.value = err.response.data.message
      conflictDrivers.value = err.response.data.drivers
      showConflict.value = true
    } else {
      error.value = 'Failed to delete team.'
    }
  }
}

onMounted(() => {
  fetchTeams()
})
</script>

<style scoped>
.success-toast {
  position: fixed;
  top: 2rem;
  left: 2rem;
  background-color: #4caf50;
  color: white;
  padding: 1rem 1.5rem;
  border-radius: 4px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.2);
  z-index: 2000;
}

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

.modal p {
  margin-bottom: 1rem;
}

.conflict-table {
  margin-bottom: 1rem;
  box-shadow: none;
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

label input {
  padding: 0.5rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 1rem;
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
</style>