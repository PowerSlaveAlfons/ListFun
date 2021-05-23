import { Link } from 'react-router-dom';

const Navbar = () => {
	return (
		<nav className="navbar">
			<h1>Title</h1>
			<div className="links">
				<Link to="/">Home</Link>
				<Link to="/Submit" >Submit</Link>
			</div>
		</nav>
	);
};

export default Navbar;