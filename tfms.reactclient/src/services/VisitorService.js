import axios from "axios";
import { apiUrl } from "../constants/app-constants";
// import genderOption from "../Models/GenderOption";

export async function getVisitors() {
    return await axios.get(`${apiUrl}/api/Visitors`);
}
export async function getVisitorTickets(id){
    return await axios.get(`${apiUrl}/api/Visitors/Tickets/Of/${id}`);
}
export async function getGenderOptions() {
    return await axios.get(`${apiUrl}/api/Visitors/Options/Gender`);
}

export async function getVisitorById(id){
    return await axios.get(`${apiUrl}/api/Visitors/${id}/Include`);
}

export async function postVisitor(data){
    return await axios.post(`${apiUrl}/api/Visitors`, data); 
}
export async function updateVisitor(id, data){
    console.log(data)
    return await axios.put(`${apiUrl}/api/Visitors/${id}`, data); 
}
export async function deleteVisitor(id){
    console.log(id)
    return await axios.delete(`${apiUrl}/api/Visitors/${id}`); 
}
