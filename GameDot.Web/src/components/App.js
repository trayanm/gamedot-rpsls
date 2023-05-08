import React, { Component } from 'react'
import { Container, Row, Col } from 'react-bootstrap'
import Headline from './Headline'
import Board from './Board'
import Guide from './Guide'
import WeaponsSelect from './WeaponsSelect'
import { apiServerClient } from '../lib/apiServerClient'

export default class App extends Component {
  state = {
    human: {
      name: 'This is you',
      playerWeapon: null,
      weaponSelected: false,
      wins: 0
    },
    computer: {
      name: 'Sheldon',
      playerWeapon: null,
      weaponSelected: false,
      wins: 0
    },
    ties: 0,
    start: true,
    winner: false,
    winGameTotal: 5,
    // arsenal: ['rock', 'paper', 'scissors', 'lizard', 'Spock'],
    arsenal: [],
    // playerWin: [[2, 3], [0, 4], [1, 3], [1, 4], [0, 2]],
    playerWin: [],
    results: null,
    matchRes: ''
  }

  componentDidMount = async () => {
    const apiClient = new apiServerClient();

    try {
      const dataChoices = await apiClient.getChoices();
      const arsenal = dataChoices.map(a => a.name);

      const _state = this.state;
      _state.arsenal = arsenal;
      this.setState(_state);

    } catch (err) {
      console.error(err);
    }
  };

  // randomWeapon() {
  //   const result = this.state.arsenal[Math.floor(Math.random() * this.state.arsenal.length)]
  //   return result
  // }

  tieTally() {
    this.setState({
      ties: this.state.ties + 1,
      results: [0, 0].toString()
    })
  }

  humanWinTally(playerIndex, computerIndex) {
    this.setState(prevState => ({
      human: {
        ...prevState.human,
        wins: this.state.human.wins + 1
      },
      results: [playerIndex, computerIndex].toString()
    }), () => this.gameReset(this.state.human.wins))
  }

  computerWinTally(computerIndex, playerIndex) {
    this.setState(prevState => ({
      computer: {
        ...prevState.computer,
        wins: this.state.computer.wins + 1
      },
      results: [computerIndex, playerIndex].toString()
    }), () => this.gameReset(this.state.computer.wins))
  }

  gameReset(wins) {
    if (wins === this.state.winGameTotal) {
      this.setState({ winner: true })
      setTimeout(() => {
        this.setState(prevState => ({
          human: {
            ...prevState.human,
            playerWeapon: null,
            weaponSelected: false,
            wins: 0
          },
          computer: {
            ...prevState.computer,
            playerWeapon: null,
            weaponSelected: false,
            wins: 0
          },
          ties: 0,
          start: true,
          winner: false
        }))
      }, 5000)
    } else {
      return null
    }
  }

  // headToHead = async (playerWeap, computerWeap) => {
  //   const playerIndex = this.state.arsenal.indexOf(playerWeap);
  //   const computerIndex = this.state.arsenal.indexOf(computerWeap);

  //   if (playerIndex === computerIndex) {
  //     this.tieTally(playerIndex, computerIndex)
  //   } else if (this.state.playerWin[playerIndex].includes(computerIndex)) {
  //     this.humanWinTally(playerIndex, computerIndex)
  //   } else {
  //     this.computerWinTally(computerIndex, playerIndex)
  //   }
  // }

  handleSelect = async (weapon) => {
    const playerIndex = this.state.arsenal.indexOf(weapon);

    const apiClient = new apiServerClient();
    const matchResult = await apiClient.postPlay(playerIndex + 1);
    console.log("matchResult", matchResult);

    const compWeap = this.state.arsenal[matchResult.bot - 1];

    this.setState(prevState => ({
      human: {
        ...prevState.human,
        weaponSelected: true,
        playerWeapon: weapon
      },
      computer: {
        ...prevState.computer,
        weaponSelected: true,
        playerWeapon: compWeap
      },
      start: false,
      matchRes: matchResult.results
    }), () => {
      // this.headToHead(this.state.human.playerWeapon, this.state.computer.playerWeapon)
      switch (matchResult.results) {
        case "win":
          this.humanWinTally(matchResult.player - 1, matchResult.bot - 1);
          break;
        case "lose":
          this.computerWinTally(matchResult.bot - 1, matchResult.player - 1);
          break;
        case "tie":
          this.tieTally(matchResult.player - 1, matchResult.bot - 1);
          break;

        default:
          break;
      }
    }
    )
  }

  render() {
    const { human, computer, arsenal, ties, start, results, winner, winGameTotal, matchRes } = this.state

    return (
      <Container id="top-container">
        <Row>
          <Col>
            <Headline
              arsenal={arsenal}
            />
          </Col>
        </Row>
        <Row>
          <Col>
            <Board
              human={human}
              computer={computer}
              arsenal={arsenal}
              ties={ties}
            />
          </Col>
        </Row>
        <Row>
          <Col>
          <div className='text-center'>{matchRes}</div>
            <Guide
              start={start}
              results={results}
              human={human}
              computer={computer}
              arsenal={arsenal}
              winGameTotal={winGameTotal}
            />
          </Col>
        </Row>
        <Row>
          <Col>
            <WeaponsSelect
              handleSelect={this.handleSelect}
              winner={winner}
              arsenal={arsenal}
            />
          </Col>
        </Row>
      </Container>
    )
  }
}