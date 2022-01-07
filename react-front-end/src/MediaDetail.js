import { useHistory, useParams } from "react-router-dom";
import axios from "axios";
import React from "react";
import queryString from 'query-string';
import { withRouter } from 'react-router-dom'; 
import { Media } from "react-bootstrap";


export default  withRouter(class MediaDetail extends React.Component {
    state = {
        Media: [{}],
        Entries: [{}]
    }


    componentDidMount() {
        axios.get('https://localhost:5001/media/' + this.props.match.params.id)
            .then(res => {
                const Media = res.data;
                this.setState({ Media });
                console.log(this.state.Media.map(Media => Media.name));
            }).catch(err => console.error(err))

        axios.get('https://localhost:5001/entries/' + this.props.match.params.id)
            .then(res => {
                const Entries = res.data;
                this.setState({ Entries });
            }).catch(err => console.error(err))
    }

    render() {
        return (
            <div className='GameDetail'>
                <ul>
                    <li>{this.state.Media.map(Media => Media.name)}</li>
                    <li>{this.state.Media.map(Media => Media.picture)}</li>
                </ul>                
                <ul>
                {this.state.Entries.map(Entry => <li key={Entry.timestamp}>{Entry.name} at {Entry.timestamp}</li>)}
                </ul>

            </div>
        )
    }

})