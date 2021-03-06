import axios from "axios";
import React from "react";
import { withRouter } from 'react-router-dom';
import { from, toArray, zip, map, of, switchMap, concatAll, catchError } from "rxjs";
import { Link } from 'react-router-dom';


export default withRouter(class MediaDetail extends React.Component {
    state = {
        Media: {}
    }

    componentDidMount() {
        from(axios.get('https://localhost:5001/media/' + this.props.match.params.id))
            .pipe(
                map(res => res.data),
                switchMap(media => zip(of(media), this.getAllEntries(this.props.match.params.id))),
                map(([media, Entries]) => {
                    media["Entries"] = Entries;
                    return media
                })
            ).subscribe({
                next: Media => {
                    this.setState({ Media });
                },
                error: err => console.error(err),
                complete: () => console.info('feddish')
            });

    }

    render() {
        if (!this.state.Media.Entries)
            return (<div>Loading Entries</div>);
        return (
            <div className='GameDetail'>
                <div>
                        <Link to={`/addEntry/${this.state.Media.id}`}>Add new Entry for this Media</Link>
                </div>
                <ul>
                    <li>{this.state.Media.name}</li>
                    <li>{this.state.Media.picture}</li>
                </ul>
                <ul>
                    {this.state.Media.Entries.map(Entry => <li key={Entry.timestamp}>{Entry.name} at {Entry.timestamp}</li>)}
                </ul>

            </div>
        )
    }

    getAllEntries(id) {
        return from(axios.get('https://localhost:5001/entries/' + id)).pipe(
            map(res => res.data),
            concatAll(),
            toArray(),
            catchError(Error => {
                return of([]);
            }));
    }

})