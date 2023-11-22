import React, { Component } from 'react';
import { Navigate } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext'; // Asegúrate de importar el contexto aquí
import './index.css';

export class Registration extends Component {
  static contextType = AuthContext;

  constructor(props) {
    super(props);
    this.state = {
      userInfo: {
        Nombres: '',
        Apellidos: '',
        NumeroIdentificacion: '',
        Email: '',
        TipoIdentificacion: '',
      },
      userData: {
        NombreUsuario: '',
        Contraseña: '',
      },
      error: '',
      redirectToUsers: false,
    };
  }

  handleInputChange = (e) => {
    const { name, value } = e.target;
    this.setState((prevState) => ({
      userInfo: {
        ...prevState.userInfo,
        [name]: value,
        NumeroIdentificacionTipoConcatenado:
          name === 'TipoIdentificacion' || name === 'NumeroIdentificacion'
            ? prevState.userInfo.TipoIdentificacion + '-' + prevState.userInfo.NumeroIdentificacion
            : prevState.userInfo.NumeroIdentificacionTipoConcatenado,
        NombresApellidosConcatenados:
          name === 'Nombres' || name === 'Apellidos'
            ? prevState.userInfo.Nombres + ' ' + prevState.userInfo.Apellidos
            : prevState.userInfo.NombresApellidosConcatenados,
      },
    }));
  };

  handleInputChangeUserData = (e) => {
    const { name, value } = e.target;
    this.setState((prevState) => ({
      userData: {
        ...prevState.userData,
        [name]: value,
      },
    }));
  };

  handleRegistration = async () => {
    const { userData, userInfo } = this.state;
    

    try {
      const response = await fetch('https://localhost:44480/users/persona', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(userInfo),
      });

      if (response.ok) {
        const data = await response.json();
        const combinedData = {
          ...userData,
          PersonaId: data.id,
        };

        try {
          const response = await fetch('https://localhost:44480/users/users', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(combinedData),
          });

          if (response.ok) {
            this.setState({ redirectToUsers: true });
          } else {
            throw new Error('Error en la petición');
          }
        } catch (error) {
          console.error('Error al enviar datos al servidor:', error);
          this.setState({ error: 'Error al enviar datos al servidor' });
        }
      } else if (response.status === 404) {
        // Si el usuario no existe, redirigir a una ruta específica (por ejemplo, '/Redirect')
        this.setState({ error: 'El usuario ya existe' });
      } else {
        throw new Error('Error en la petición');
      }
    } catch (error) {
      console.error('Error al enviar datos al servidor:', error);
      this.setState({ error: 'Error al enviar datos al servidor' });
    }
    console.log('Datos de usuario registrados:', userInfo);
  };

  render() {
    const { userData, userInfo, error, redirectToUsers } = this.state;

    if (redirectToUsers) {
      return <Navigate to="/" />;
    }

    return (
      <div className="form-container">
        <h2>Registro de Usuario</h2>
        {error && <p className="error-message">{error}</p>}
        <form>
          <div className="form-field">
            <label>Tipo identificación:</label>
            <input
              type="text"
              name="TipoIdentificacion"
              value={userInfo.TipoIdentificacion}
              onChange={this.handleInputChange}
            />
          </div>
          <div className="form-field">
            <label>Nombres:</label>
            <input
              type="text"
              name="Nombres"
              value={userInfo.Nombres}
              onChange={this.handleInputChange}
            />
          </div>
          <div className="form-field">
            <label>Apellidos:</label>
            <input
              type="text"
              name="Apellidos"
              value={userInfo.Apellidos}
              onChange={this.handleInputChange}
            />
          </div>
          <div className="form-field">
            <label>Número de identificación:</label>
            <input
              type="text"
              name="NumeroIdentificacion"
              value={userInfo.NumeroIdentificacion}
              onChange={this.handleInputChange}
            />
          </div>
          <div className="form-field">
            <label>Email:</label>
            <input
              type="text"
              name="Email"
              value={userInfo.Email}
              onChange={this.handleInputChange}
            />
          </div>
          <div className="form-field">
            <label>Nombre de Usuario:</label>
            <input
              type="text"
              name="NombreUsuario"
              value={userData.NombreUsuario}
              onChange={this.handleInputChangeUserData}
            />
          </div>
          <div className="form-field">
            <label>Contraseña:</label>
            <input
              type="password"
              name="Contraseña"
              value={userData.Contraseña}
              onChange={this.handleInputChangeUserData}
            />
          </div>

          <button className="submit-button" type="button" onClick={this.handleRegistration}>
            Registrar
          </button>
        </form>
      </div>
    );
  }
}

