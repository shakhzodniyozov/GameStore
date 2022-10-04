import { axios } from "../axios/axios";

class GameService {
    #baseUrl = "games";

    async getPagedGames(page = 1) {
        return await axios.get(`${this.#baseUrl}?page=${page}`);
    }

    async getById(id) {
        return await axios.get(`${this.#baseUrl}/${id}`);
    }

    async add(game) {
        return await axios.post(this.#baseUrl, game);
    }

    async update(game) {
        return await axios.put(this.#baseUrl, game);
    }

    async filter(filter) {
        return await axios.post(`${this.#baseUrl}/filter`, filter);
    }

    async delete(id) {
        return await axios.delete(`${this.#baseUrl}/${id}`)
    }
}

export default new GameService();