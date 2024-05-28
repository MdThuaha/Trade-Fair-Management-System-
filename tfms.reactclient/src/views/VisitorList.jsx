import MUIDataTable from "mui-datatables";
import { deleteVisitor, getGenderOptions, getVisitorTickets, getVisitors } from "../services/VisitorService";
//import { Button, Dialog, DialogActions, DialogTitle, Fab, IconButton } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { format } from "date-fns";
import { useEffect } from "react";
import { useState } from "react";
import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Fab, IconButton, Snackbar } from "@mui/material";


const VisitorList = () => {
    const [visitors, setVisitors] = useState([]);
    const [tickets, setTickets] = useState([]);
    const [genders, setGenders] = useState([]);
    const [ticketOpen, setTicketOpen] = useState(false);
    const [alertOpen, setAlertOpen] = useState(false);
    const [delId, setDelId] = useState('');
    const [notificationOpen, setNotificationOpen] = useState(false);
    const [message, setMessage] = useState("");

    useEffect(() => {
        fetchVisitors();
        fetchGenders();
    }, []);
    const fetchVisitors = async () => {
        try {
            const { data } = await getVisitors();
            // console.log(data);
            setVisitors(data);
        } catch (e) {
            console.log(e);
        }
    };// fetch Visitor Data

    const fetchGenders = async () => {
        try {
            const { data } = await getGenderOptions();
            console.log(data);
            setGenders(data);
        } catch (e) {
            console.log(e);
        }
    };

    const fetchVisitorTicket = async (id) => {
        try {
            const { data } = (await getVisitorTickets(id));
            //console.log(data);
            setTickets(data);
        } catch (e) {
            console.log(e);
        }
    };// Fetch Visitor Tickets
    const getGenderName = (v) => {
        switch (v) {
            case 1: return 'Male';
            case 2: return 'Female';
            default: return 'Others'
        }
    };

    const showTicketClick = (id) => {
        fetchVisitorTicket(id);
        setTicketOpen(true);
    };

    const doDeleteVisitors = async (id) => {
        try {
            const {res} = await deleteVisitor(delId);
            const v = visitors.filter(x=> x.visitorId == delId);
            setVisitors(v);
            setDelId('');
            setAlertOpen(false);
            setMessage('Data is deleted');
            setNotificationOpen(true);

            //navigate('/visitor');
        } catch (e) {
            // console.error(e);
        }
    };
    const handleClose = () => {
        setTicketOpen(false);
    };

    //Alert 
    const handleAlertClose = () => {
        setAlertOpen(false)
    }
    const handleYes = () => {
        doDeleteVisitors();
    }
    const handleNotificationClose = () => {
        setNotificationOpen(false);
    };

    const columns = [

        {
            name: "firstName",
            label: "Frist Name",
            options: {
                filter: true,
                sort: true,
            },
        },
        {
            name: "lastName",
            label: "Last Name",
            options: {
                filter: true,
                sort: true,
            },
        },
        {
            name: "email",
            label: "Email",
            options: {
                filter: true,
                sort: true,
            },
        },
        {
            name: "phone",
            label: "Phone",
            options: {
                filter: true,
                sort: true,
            },
        },
        {
            name: "gender",
            label: "Gender",
            options: {
                filter: true,
                sort: true,
                customBodyRender: (value, tableMeta, updateValue) => {
                    console.log(value)
                    return (
                        <>{getGenderName(value)}</>
                    )
                }
            },

        },
        {
            name: "nationality",
            label: "Nationality",
            options: {
                filter: true,
                sort: true,
            },
        },
        {
            name: 'visitorId',
            label: 'Ticket',
            options: {
                filter: false,
                sort: false,
                customBodyRender: (value, tableMeta, updateValue) => {
                    return (
                        <Button onClick={() => showTicketClick(value)} variant="text">Ticket</Button>
                    )
                }
            },

        },
     

        {
            name: "visitorId",
            label: "Action",
            options: {
                filter: false,
                sort: false,
                customBodyRender: (value, tableMeta, updateValue) => {
                    return (
                        <>
                            <IconButton href={`/visitor-edit/${value}`} color="primary" variant="contained"><EditIcon /></IconButton>
                            
                            <IconButton onClick={() => {
                                setDelId(value);
                                setAlertOpen(true)
                            }} color="error">
                                <DeleteIcon />
                            </IconButton>
                        </>
                    );
                },
            },
        },

    ];
    const ticketsColumns = [
        {
            name: 'issueDate',
            label: 'Issue Date',
            type: 'date',

            options: {
                filter: true,
                sort: true,
                customBodyRender: (value, tableMeta, updateValue) => {
                    return (
                        <span>{format(value, "yyyy-MM-dd")}</span>
                    );
                },

            }
        },
        {
            name: 'quantity',
            label: 'Quantity',
            options: {
                filter: false,
                sort: false
            }
        },
        {
            name: 'price',
            label: 'Price',
            options: {
                filter: false,
                sort: false
            }
        }
    ];
    return (
        <>
            <MUIDataTable title={"Visitors with Tickets List"} data={visitors} columns={columns}
                options={{
                    selectableRows: false, hover: false,
                    customToolbar: () => <Fab sx={{ ml: 1 }} variant="extended" size="medium" href="/visitor-create" color="primary"><AddIcon sx={{ mr: 1 }} /> Add New</Fab>,


                    //customToolbar: () => <IconButton href="/visitor-create" color="primary"><AddIcon /></IconButton>,
                }}
            />
            <Dialog open={ticketOpen} fullWidth={true}>
                <DialogTitle>Visitor Ticket</DialogTitle>
                <MUIDataTable title={'Tickets'} columns={ticketsColumns} data={tickets}
                    options={{
                        selectableRows: false,
                        elevation: 0
                    }}
                />
                <DialogActions>
                    <Button onClick={handleClose}>Close</Button>
                </DialogActions>
            </Dialog>

            <Dialog
                open={alertOpen}
                onClose={handleAlertClose}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title">
                    {"Delete item?"}
                </DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        Are you sure to delete this item?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleAlertClose}>No</Button>
                    <Button onClick={handleYes} autoFocus>
                        Yes
                    </Button>
                </DialogActions>
            </Dialog>
            <Snackbar
                open={notificationOpen}
                autoHideDuration={2000}
                message={message}
                action={"DISMISS"}
                onClose={handleNotificationClose}
            />
        </>
    )
}

export default VisitorList;