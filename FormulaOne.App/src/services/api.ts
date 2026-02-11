import axios from 'axios'

const api = axios.create({
  baseURL: 'https://localhost:7009/api',
  headers: {
    'Content-Type': 'application/json'
  }
})

// Teams
export const getTeams = (search?: string) =>
  api.get('/teams', { params: { search } })

export const getTeam = (id: number) =>
  api.get(`/teams/${id}`)

export const createTeam = (data: object) =>
  api.post('/teams', data)

export const updateTeam = (id: number, data: object) =>
  api.put(`/teams/${id}`, data)

export const deleteTeam = (id: number) =>
  api.delete(`/teams/${id}`)

// Drivers
export const getDrivers = (search?: string, sortBy?: string) =>
  api.get('/drivers', { params: { search, sortBy } })

export const getDriver = (id: number) =>
  api.get(`/drivers/${id}`)

export const createDriver = (data: object) =>
  api.post('/drivers', data)

export const updateDriver = (id: number, data: object) =>
  api.put(`/drivers/${id}`, data)

export const deleteDriver = (id: number) =>
  api.delete(`/drivers/${id}`)

// Circuits
export const getCircuits = () =>
  api.get('/circuits')