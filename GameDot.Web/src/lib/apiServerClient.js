export class apiServerClient {
    constructor() {
        this.baseUrl = process.env.REACT_APP_BASE_URL;
    }

    // api/choices
    async getChoices() {
        let result = [];
        const url = `${this.baseUrl}/choices`;

        await fetch(url, {
            method: "GET",
            mode: "cors",
            headers: {
                "Content-Type": "application/json"
            }
        })
            .then(response => response.json())
            .then(data => {
                result = data;
            })
            .catch(err => {
                console.error(err);
            });

        return result;
    }

    // api/choice
    async getChoice() {
        let result = null;
        const url = `${this.baseUrl}/choice`;

        await fetch(url, {
            method: "GET",
            mode: "cors",
            headers: {
                "Content-Type": "application/json"
            }
        })
            .then(response => response.json())
            .then(data => {
                result = data;
            })
            .catch(err => {
                console.error(err);
            });

        return result;
    }

    // api/play
    async postPlay(choiceId) {
        let result = null;
        const url = `${this.baseUrl}/play`;

        const dto = {
            player: choiceId
        }

        await fetch(url, {
            method: "POST",
            mode: "cors",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(dto)
        })
            .then(response => response.json())
            .then(data => {
                result = data;
            })
            .catch(err => {
                console.error(err);
            });

        return result;
    }
}
