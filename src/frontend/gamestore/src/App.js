import './App.css';
import { Routes, Route } from 'react-router-dom';
import { WithNavbar } from './Pages/WithNavbar';


function App() {
  return (
    <Routes>
      <Route path="/*" element={<WithNavbar />} />
    </Routes>
  );
}

export default App;
