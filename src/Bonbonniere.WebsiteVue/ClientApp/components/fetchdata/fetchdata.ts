import Vue from 'vue';
import { Component } from 'vue-property-decorator';

interface User {
    email: string;
	username: string;
	gender: string;
	address: string;
}

@Component
export default class FetchDataComponent extends Vue {
	users: User[] = [];

    mounted() {
		fetch('api/user/getall')
			.then(response => response.json() as Promise<User[]>)
            .then(data => {
				this.users = data;
            });
    }
}
