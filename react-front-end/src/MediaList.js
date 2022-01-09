import React from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { concatAll, concatMap, map, zip, from, of, toArray, reduce, catchError } from 'rxjs';


export default class MediaList extends React.Component {
    state = {
        Media: [{}]
    }

    async componentDidMount() {

        from(axios.get('https://localhost:5001/media/getAll')).pipe(
            map(res => res.data),
            concatAll(),
            concatMap(media => zip(of(media), this.getLastDate(media.id))),
            map(([media, lastDate]) => {
                media["lastDate"] = lastDate;
                return media;
            }),
            toArray()
        ).subscribe({
            next: Media => this.setState({ Media }),
            error: err => console.error(err),
            complete: () => console.info('feddish')
        });
    }

    render() {
        if (!this.state.Media)
            return (<div>Loading Media</div>);
        return (
            <div className='GameList'>
                <ul>
                    {this.state.Media.map(Media =>
                        <Link to={`/media/${Media.id}`}>
                            <li key={Media.id}>{Media.name}...{Media.lastDate}</li></Link>)}
                </ul>
            </div>
        )
    }

    getLastDate(id) {
        return from(axios.get('https://localhost:5001/entries/' + id)).pipe(
            map(res => res.data),
            concatAll(),
            map(entry => entry.timestamp),
            reduce((last, Entry) => Entry > last ? Entry : last, "0001-01-01T00:00:00"),
            catchError(Error => {
                return of("No entries found.")
            })
        )

    }
}

