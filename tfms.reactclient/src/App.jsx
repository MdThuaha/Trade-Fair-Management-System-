import { Box } from '@mui/material';
import { Routes, Route } from 'react-router-dom';
import NavBarComponent from './components/NavBarComponent';
import HomeComponent from './components/HomeComponent';
import './App.css'
import VisitorList from './views/VisitorList';
import VisitorCreateForm from './views/VisitorCreateForm';
import VisitorEditForm from './views/VisitorEditForm';

function App() {
  return (
      <>
          <NavBarComponent></NavBarComponent>
          <Box sx={{ px: 2, py: 1 }}>
              <Routes>
                  <Route path="/" element={<HomeComponent />} />
                <Route path='/Visitors' element={<VisitorList/>} />
                <Route path='/visitor-create' element={<VisitorCreateForm/>} />
                <Route path='/visitor-edit/:id' element={<VisitorEditForm/>} />
              </Routes>
          </Box>
    </>
  )
}
export default App
