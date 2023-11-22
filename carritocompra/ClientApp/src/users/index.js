import React, { Component } from 'react';

export class UserRegistration extends Component {
  
  constructor(props) {
    
    super(props);
    console.log(this.props.location);
    this.state = {
      userInfo: this.props.location.state?.userInfo ,
      userData: {
        NombreUsuario: '',
        Contraseña: '',
        Identificador: '',
      },
      error: '',
    };
  }

  handleInputChange = (e) => {
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
    if (!userInfo || Object.keys(userInfo).length === 0) {
      this.setState({ error: 'La información de usuario es nula o indefinida' });
      return;
    }
    const combinedData = {
      ...userInfo,
      ...userData,
    };

    try {
      const response = await fetch('https://localhost:44480/users/persona', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(combinedData.userInfo),
      });

      if (response.ok) {
        const data = await response.json();
        console.log(data);
        //const personaId = data.Id;
      } else {
        throw new Error('Error en la petición');
      }
    } catch (error) {
      console.error('Error al enviar datos al servidor:', error);
      this.setState({ error: 'Error al enviar datos al servidor' });
    }

    try {
      const response = await fetch('https://localhost:44480/users/users', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(combinedData),
      });

      if (response.ok) {
        // Success logic if needed
      } else {
        throw new Error('Error en la petición');
      }
    } catch (error) {
      console.error('Error al enviar datos al servidor:', error);
      this.setState({ error: 'Error al enviar datos al servidor' });
    }
  };

  render() {
    const { userData, error } = this.state;
    if (!this.props.location.state || !this.props.location.state.userInfo) {
      return <div>Error: No se proporcionó la información de usuario necesaria</div>;
    }
    return (
      <div>
        <h2>Registro de Usuario</h2>
        {error && <p style={{ color: 'red' }}>{error}</p>}
        <form>
          <div>
            <label>Nombre de Usuario:</label>
            <input
              type="text"
              name="NombreUsuario"
              value={userData.NombreUsuario}
              onChange={this.handleInputChange}
            />
          </div>
          <div>
            <label>Contraseña:</label>
            <input
              type="password"
              name="Contraseña"
              value={userData.Contraseña}
              onChange={this.handleInputChange}
            />
          </div>
          <button type="button" onClick={this.handleRegistration}>
            Registrar Usuario
          </button>
        </form>
      </div>
    );
  }
}
