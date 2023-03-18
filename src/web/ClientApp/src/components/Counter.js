import React from 'react';
import {
    useQuery,
} from "@tanstack/react-query";
import axios from "axios";
import authService from './api-authorization/AuthorizeService'

export function Counter(props) {
    const { isLoading, error, data, isFetching } = useQuery({
        queryKey: ["messages"],
        queryFn: async () => {
            const token = await authService.getAccessToken();
            return axios
                .get("api/chat/general", { headers: !token ? {} : { 'Authorization': `Bearer ${token}` }  })
                .then((res) => res.data);
        },
    });

    if (isLoading || isFetching) return "Loading...";

    if (error) return "An error has occurred: " + error.message;

    return JSON.stringify(data);
}