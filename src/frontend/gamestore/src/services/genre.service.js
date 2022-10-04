import { axios } from "../axios/axios";

class GenreService {
    #baseUrl = "genre";

    async getAll() {
        return await axios.get(this.#baseUrl);
    }

    async getAllWithDetails() {
        return await axios.get(`${this.#baseUrl}/details`);
    }
}

export default new GenreService();