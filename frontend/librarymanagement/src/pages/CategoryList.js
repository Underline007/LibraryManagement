import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, Form, Input, Pagination, Spin, message } from 'antd';
import { useCategoryContext } from '../context/CategoryContext';

const CategoryList = () => {
    const { categories, totalPages, loading, fetchCategories, addCategory, updateCategory, deleteCategory } = useCategoryContext();
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [isEdit, setIsEdit] = useState(false);
    const [currentCategory, setCurrentCategory] = useState(null);
    const [form] = Form.useForm();
    const [currentPage, setCurrentPage] = useState(1);

    useEffect(() => {
        fetchCategories(currentPage - 1, 10);
    }, [currentPage]);

    const showModal = () => {
        setIsEdit(false);
        setCurrentCategory(null);
        form.resetFields();
        setIsModalVisible(true);
    };

    const showEditModal = (category) => {
        setIsEdit(true);
        setCurrentCategory(category);
        form.setFieldsValue(category);
        setIsModalVisible(true);
    };

    const handleOk = () => {
        form.validateFields().then(values => {
            if (isEdit && currentCategory) {
                updateCategory(currentCategory.id, values);
            } else {
                addCategory(values);
            }
            form.resetFields();
            setIsModalVisible(false);
        }).catch(info => {
            console.log('Validate Failed:', info);
        });
    };

    const handleCancel = () => {
        setIsModalVisible(false);
        form.resetFields();
    };

    const handlePageChange = (page) => {
        setCurrentPage(page);
    };

    const handleDelete = (id) => {
        Modal.confirm({
            title: 'Are you sure you want to delete this category?',
            onOk: async () => {
                await deleteCategory(id);
                message.success('Category deleted successfully');
            },
        });
    };

    const columns = [
        {
            title: 'Name',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: 'Actions',
            key: 'actions',
            render: (_, record) => (
                <span>
                    <Button onClick={() => showEditModal(record)} type="primary" style={{ marginRight: 8 }}>
                        Edit
                    </Button>
                    <Button onClick={() => handleDelete(record.id)} style={{ marginRight: 10, backgroundColor: 'red', color: 'white' }} >
                        Delete
                    </Button>
                </span>
            ),
        },
    ];

    if (loading) {
        return <Spin />;
    }

    return (
        <div style={{ padding: 20 }}>
            <Button type="primary" onClick={showModal} style={{ marginBottom: 8 }}>
                Add Category
            </Button>
            <Table dataSource={categories} columns={columns} rowKey="id" pagination={false} style={{ backgroundColor: '#f0f2f5', borderRadius: 4 }} /> {/* Basic table styling */}
            <Pagination
                current={currentPage}
                total={totalPages * 10}
                onChange={handlePageChange}
                style={{ marginTop: 16 }}
            />
            <Modal title={isEdit ? "Edit Category" : "Add Category"} visible={isModalVisible} onOk={handleOk} onCancel={handleCancel}>
                <Form form={form} layout="vertical">
                    <Form.Item
                        name="name"
                        label="Category Name"
                        rules={[{ required: true, message: 'Please input the category name!' }]}
                    >
                        <Input />
                    </Form.Item>
                </Form>
            </Modal>
        </div>
    );
};

export default CategoryList;
