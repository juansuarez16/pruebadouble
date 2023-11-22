import React, { Component } from "react";
import { Navigate } from "react-router-dom";
import { AuthContext } from "../context/AuthContext"; // Asegúrate de importar el contexto aquí
import './index.css';

export class Login extends Component {
  static contextType = AuthContext;

  constructor(props) {
    super(props);

    this.state = {
      error: "",
      NombreUsuario: "",
      Contraseña: "",
      userNoexist:false
    };
  }

  handleInputChange = (event) => {
    const { name, value } = event.target;
    this.setState({ [name]: value });
  };

  handleLogin = async (event) => {
    event.preventDefault();
    const { NombreUsuario, Contraseña } = this.state;
    const { login } = this.context;

    try {
      const response = await fetch("https://localhost:44480/login/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ NombreUsuario, Contraseña }),
      });

      if (response.ok) {
        // Si el inicio de sesión es exitoso, llamar al método login del contexto
        login();
      } else if (response.status === 404) {
        // Si el usuario no existe, mostrar mensaje de error
        
        this.setState({
          error: "El usuario no existe",
          userNoexist:true
        });
      } else {
        // Si la solicitud falla o las credenciales son incorrectas, mostrar error
        this.setState({ error: "Usuario o contraseña incorrectos" });
      }
    } catch (error) {
      console.error("Error de inicio de sesión:", error);
      this.setState({ error: "Error al iniciar sesión" });
    }
  };

  render() {
    const { error, NombreUsuario, Contraseña,userNoexist } = this.state;
    const { isAuthenticated } = this.context;
    console.log(isAuthenticated);
    if (isAuthenticated) {
      return <Navigate to="/Dashboard" />;
    }
    if (userNoexist) {
      return <Navigate to="/Registre" />;
    }
    return (
      <div className="login-container">
        <h2>Iniciar sesión</h2>
        {error && <p className="error-message">{error}</p>}
        <form onSubmit={this.handleLogin} className="login-form">
          <label htmlFor="username">Usuario:</label>
          <input
            type="text"
            id="username"
            name="NombreUsuario"
            value={NombreUsuario}
            onChange={this.handleInputChange}
            className="input-field"
          />

          <label htmlFor="password">Contraseña:</label>
          <input
            type="password"
            id="password"
            name="Contraseña"
            value={Contraseña}
            onChange={this.handleInputChange}
            className="input-field"
          />

          <button type="submit" className="login-button">
            Iniciar sesión
          </button>
        </form>
      </div>
    );
  }
}
