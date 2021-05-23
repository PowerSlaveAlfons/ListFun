import MediaList from "./MediaList.js";
import Submit from "./Submit.js"
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import Navbar from "./Navbar.js"


function App() {
  return (
    <Router>
      <div className="App">
        <Navbar />
        <Route exact path="/">
          <MediaList />
        </Route>
        <div className="Submit">
          <Route path="/Submit">
            <Submit />
          </Route>
        </div>
      </div>
    </Router>
  );
}

export default App;
