import React from 'react';
import { Layout } from 'antd';
import AppHeader from './Header';
import AppFooter from './Footer';

const { Content } = Layout;

const MainLayout = ({ children }) => {
    return (
        <Layout style={{ minHeight: '100vh' }}>
            <AppHeader />
            <Content style={{ padding: '0 50px', flex: '1 0 auto' }}>
                <div className="site-layout-content" style={{ padding: 24 }}>
                    {children}
                </div>
            </Content>
            <AppFooter />
        </Layout>
    );
};

export default MainLayout;
