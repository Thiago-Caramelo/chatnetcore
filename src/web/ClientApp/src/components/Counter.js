import React, { useState } from 'react';

export function Counter(props) {
    const [count, setCount] = useState(0);
    return <h1>ula la la, {props.name}</h1>;
}