import React from 'react';
import { useHistory } from 'react-router';
import { useState } from 'react';
import axios from 'axios';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { useParams } from "react-router-dom";


export default function AddEntry() {
	const history = useHistory();
	const [name, setName] = useState('');
	const [picture, setPicture] = useState('');
	const [timestamp, setTimeStamp] = useState('');
	const [comments, setComments] = useState('');
	const id = useParams()

	const handleSubmit = (e) => {
		e.preventDefault();
		const Entry = {
			timestamp,
			comments,
			name,
			picture
		};
		Entry["parentid"] = 0
		Entry["mediaid"] = id["id"]
		console.log(id)
		axios({
			method: 'post',
			url: 'https://localhost:5001/entries/add',
			data: Entry
		}).then(() => { history.push('/'); });
	};


	return (
		<div className="AddEntry">
			<h2>Submit new Entry</h2>
			<form onSubmit={handleSubmit}>
				<label>Entry Title:</label>
				<input
					type="text"
					required
					value={name} onChange={(e) => setName(e.target.value)}
				/>
				<br></br>
				<label>Timestamp:</label>
				<DatePicker selected={timestamp} onChange={(e) => setTimeStamp(e)}></DatePicker>
				<br></br>
				<label>Picture URL:</label>
				<input
					type="text"
					required
					value={picture} onChange={(e) => setPicture(e.target.value)}
				/>
				<br></br>
				<label>Comments:</label>
				<input
					type="text"
					required
					value={comments} onChange={(e) => setComments(e.target.value)}
				/>
				<br></br>
				<button>Add Entry</button>
			</form>
		</div>
	);
}
