import React from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';


export default class MediaList extends React.Component {
    state = {
        Media: [{}]
    }

    componentDidMount() {
        axios.get('http://localhost:50352/media/getAll')
            .then(res => {
                const Media = res.data;
                this.setState({ Media });
            }).catch(err => console.error(err))
    }

    render() {
        return (
            <div className='GameList'>
                <ul>
                    {this.state.Media.map(Media => 
                    <Link to={`/media/${Media.id}`}>
                        <li key={Media.Id}>{Media.name}</li></Link>)}
                </ul>
            </div>
        )
    }

}