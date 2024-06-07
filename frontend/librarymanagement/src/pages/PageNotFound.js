
import React from 'react';
import { Result, Button } from 'antd';
import { Link } from 'react-router-dom';

const PageNotFound = () => {
    return (
        <Result
            status="404"
            title="404"
            subTitle="Sorry, the page you visited does not exist or your account do not have accessibility to this page"
            extra={<Button type="primary"><Link to="/">Back Home</Link></Button>}
        />
    );
};

export default PageNotFound;
