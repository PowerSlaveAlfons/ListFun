import React from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { concatAll, concatMap, map, zip, from, of, toArray } from 'rxjs';


export default class MediaList extends React.Component {
    state = {
        Media: [{}],
        Entries: [{}]
    }

    async componentDidMount() {
        /*
        await axios.get('https://localhost:5001/media/getAll')
            .then(res => {
                const Media = res.data;
                this.setState({ Media });
            }).catch(err => console.error(err)) */

        from(axios.get('https://localhost:5001/media/getAll')).pipe(
            map(res => res.data),
            concatAll(),
            concatMap(media => {
                return zip( of(media), axios.get('https://localhost:5001/entries/' + media.id))
            }),
            map(([media, res]) => {
                media["lastDate"] = res.data.map(entry => entry.timestamp).reduce((last, Entry) => Entry > last ? Entry : last, "0001-01-01T00:00:00");
                return media;
            }), toArray()
        ).subscribe({next: Media => this.setState({ Media }), error: err => console.error(err), complete: () => console.info('feddish') });
        /*
        for (const index in this.state.Media) {
            await axios.get('https://localhost:5001/entries/' + this.state.Media[index].id)
                .then(res => {
                    const Entries = res.data;
                    this.setState({ Entries })
                }).catch(err => console.error(err))
            this.state.Media[index]["lastDate"] = this.state.Entries.map(entry => entry.timestamp).reduce((last, Entry) => Entry > last ? Entry : last, "0001-01-01T00:00:00")
            console.log(index)
        }
        this.setState(this.state.Media)
        */
    }

    render() {
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

}