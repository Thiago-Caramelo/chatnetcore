import React, { useState } from 'react';
import { Input, Button } from 'reactstrap';
import {
    useQuery, useQueryClient
} from "@tanstack/react-query";
import axios from "axios";
import authService from './api-authorization/AuthorizeService'

export function Counter(props) {
    const queryClient = useQueryClient();
    const [text, setText] = useState('');
    const { isLoading, error, data, isFetching } = useQuery({
        queryKey: ["messages"],
        queryFn: async () => {
            const token = await authService.getAccessToken();
            return axios
                .get("https://localhost:7077/Chat", { headers: !token ? {} : { 'Authorization': `Bearer ${token}` } })
                .then((res) => res.data);
        },
    });

    const handleInputChange = (event) => {
        setText(event.target.value);
    };
    const handleClick = async () => {
        const token = await authService.getAccessToken();
        const user = await authService.getUser();

        if (!user || !token) return false;

        const userName = user.name;
        await axios.post("https://localhost:7077/Chat", { userName, text }, { headers: !token ? {} : { 'Authorization': `Bearer ${token}` } });
        queryClient.invalidateQueries({ queryKey: ['messages'] });
        setText('');
    };

    if (isLoading || isFetching) return "Loading...";

    if (error) return "An error has occurred: " + error.message;

    const renderedArray = data.map((item, index) => (
        <h6 key={index}><span className="badge bg-secondary">{item.userName}</span> {"   " + item.text + "   "}</h6>
    ));

    return (<div>
        <div className="input-group">
            <Input type="text" value={text} onChange={handleInputChange} className="form-control"></Input>
            <Button className="btn btn-outline-secondary" type="button" onClick={handleClick}>Send</Button>
        </div>
        <br />
        <br />
        <div>
            {renderedArray}
        </div>

     </div>);
}