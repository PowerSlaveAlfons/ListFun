import React from 'react';
import { useHistory } from 'react-router';
import { useState } from 'react';
import axios from 'axios';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';


export default function Submit() {
	const history = useHistory();
	const [name, setName] = useState('');
	const [type, setType] = useState('Game');
	const [picture, setPicture] = useState('');
	const [releaseDate, setReleaseDate] = useState('');

	const handleSubmit = (e) => {
		e.preventDefault();
		const Media = { name, picture, type, releaseDate };

		axios({
			method: 'post',
			url: 'https://localhost:5001/media/add',
			data: Media
		}).then(() => { history.push('/'); });
	};


	return (
		<div className="submit">
			<h2>Submit new Media</h2>
			<form onSubmit={handleSubmit}>
				<label>Media Title:</label>
				<input
					type="text"
					required
					value={name} onChange={(e) => setName(e.target.value)}
				/>
				<br></br>
				<label>Picture URL:</label>
				<input
					type="text"
					required
					value={picture} onChange={(e) => setPicture(e.target.value)}
				/>
				<br></br>
				<label>Release Date</label>
				<DatePicker selected={releaseDate} onChange={(e) => setReleaseDate(e)}></DatePicker>
				<br></br>
				<label>Type:</label>
				<select
					value={type}
					onChange={(e) => setType(e.target.value)}>
					<option value="Game">Game</option>
					<option value="Movie">Movie</option>
				</select> <br></br>
				<button>Add Game</button>
			</form>
		</div>
	);
}
