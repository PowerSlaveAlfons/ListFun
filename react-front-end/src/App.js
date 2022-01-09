import MediaList from "./MediaList.js";
import Submit from "./Submit.js"
import AddEntry from "./AddEntry.js"
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import Navbar from "./Navbar.js";
import MediaDetail from "./MediaDetail.js";



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
          <Route path="/media/:id" component="MediaDetail">
            <MediaDetail />
          </Route>
          <Route path="/addEntry/:id" component="AddEntry">
            <AddEntry />
          </Route>
        </div>
      </div>
    </Router>
  );
}

export default App;
