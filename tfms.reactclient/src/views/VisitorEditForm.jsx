import {
    Avatar,
    Button,
    Container,
    FormHelperText,
    Grid,
    IconButton,
    InputAdornment,
    List,
    ListItem,
    ListItemText,
    InputLabel,
    Select,
    MenuItem,
    Snackbar,
    TextField,
    Typography,
} from "@mui/material";
import {  format } from "date-fns";
import AddIcon from "@mui/icons-material/Add";
import DeleteIcon from "@mui/icons-material/Delete";
import { ErrorMessage, Field, FieldArray, Form, Formik } from "formik";
import { useEffect, useState, useRef } from "react";
import * as Yup from "yup";
import { useParams } from "react-router-dom";
import { getGenderOptions, getVisitorById, updateVisitor } from "../services/VisitorService";
const VisitorEditForm = () => {
    //States
    ////////////////////////////////////////////////

    const [notificationOpen, setNotificationOpen] = useState(false);
    const [message, setMessage] = useState("");
    const [visitor, setvisitor] = useState([]);
    //Route params
    /////////////////////////////////
    const { id } = useParams();

    const validationSchema = Yup.object({
        firstName: Yup.string().required("First name is requred"),
        lastName: Yup.string().required("Last name is requred"),
        email: Yup.string().required("Email is requred"),
        phone: Yup.string().required("Phone is requred"),
        gender: Yup.number().required('Gender is required'),
        nationality: Yup.string().required("Nationality is requred"),
        tickets: Yup.array().of(
            Yup.object().shape({
                issueDate: Yup.date().required("Issue Date is required"),
                quantity: Yup.number().required("Quantity is required"),
                price: Yup.number().required("Price is required"),
            })
        ),
    });
    const initialValues = {
        ...visitor,
    };
    // Data hooks
    useEffect(() => {
        fetchVisitor();
        
    }, []);
  

    const fetchVisitor = async () => {
        console.log(id);

        const { data } = await getVisitorById(id);
       //data.issueDate = format(new Date(data.issueDate), "yyyy-MM-dd");
       //const x= visitor.issueDate.indexOf("T")
        for(var i=0; i < data.tickets.length; i++){
            data.tickets[i].issueDate = format(data.tickets[i].issueDate, "yyyy-MM-dd")
        }
        setvisitor(data);
        console.log("visitor ", data);
    };
    //Handlers
    ////////////////////////////////////
    const handleSubmit = async (values, { setSubmitting, resetForm }) => {
        console.log(values);
        const { data } = await updateVisitor(visitor.visitorId, {
            visitorId: visitor.visitorId,
            firstName: values.firstName,
            lastName: values.lastName,
            email: values.email,
            phone: values.phone,
            gender: values.gender,
            nationality: values.nationality,
           

            tickets: [...values.tickets],
        });
        setMessage("Data Update successfully");
        setNotificationOpen(true);
    };
    const handleClose = () => {
        setNotificationOpen(false);
    };
    const getDate = (v)=>{
        
        return format(new Date(v), "yyyy-MM-dd")
    }
    return (
        <Container>
            <Formik
                initialValues={initialValues}
                validationSchema={validationSchema}
                onSubmit={handleSubmit}
                enableReinitialize={true}
            >
                {({ isSubmitting, setFieldValue, values, handleChange }) => {
                    return (
                        <Form>
                            <Grid container spacing={2}>
                                <Grid item xs={12}>
                                    <Typography variant="h3">Visitors form</Typography>
                                </Grid>

                                <Grid item xs={6}>
                                    <Field
                                        as={TextField}
                                        fullWidth={true}
                                        name="firstName"
                                        variant="standard"
                                        helperText={<ErrorMessage name="firstName" />}
                                    />
                                </Grid>
                                <Grid item xs={6}>
                                    <Field
                                        as={TextField}
                                        fullWidth={true}
                                        name="lastName"
                                        variant="standard"
                                        helperText={<ErrorMessage name="lastName" />}
                                    />
                                </Grid>
                                <Grid item xs={6}>
                                    <Field
                                        as={TextField}
                                        fullWidth={true}
                                        name="email"
                                        variant="standard"
                                        helperText={<ErrorMessage name="email" />}
                                    />
                                </Grid>
                                <Grid item xs={6}>
                                    <Field
                                        as={TextField}
                                        fullWidth={true}
                                        name="phone"
                                        variant="standard"
                                        helperText={<ErrorMessage name="phone" />}
                                    />
                                </Grid>
                                <Grid item xs={6}>
                                    <Field
                                        as={TextField}
                                        fullWidth={true}
                                        name="gender"
                                        
                                        variant="standard"
                                        defaultValue={""}
                                        value={values.gender ?? ""}
                                        select
                                        onChange={(e) =>
                                            (values.gender = e.target.value ?? "")
                                        }
                                    >

                                        <MenuItem value={1}>Male</MenuItem>
                                        <MenuItem value={2}>Female</MenuItem>
                                        <MenuItem value={3}>Others</MenuItem>

                                    </Field>
                                    <FormHelperText>
                                        <ErrorMessage name="gender" />
                                    </FormHelperText>
                                </Grid>

                                <Grid item xs={6}>
                                    <Field
                                        as={TextField}
                                        fullWidth={true}
                                        name="nationality"
                                        
                                        variant="standard"
                                        helperText={<ErrorMessage name="nationality" />}
                                    />
                                </Grid>



                                <FieldArray name="tickets">
                                    {(arrayHelpers) => {
                                        return (
                                            <>
                                                <Grid item xs={12}>
                                                    <div
                                                        style={{
                                                            display: "flex",
                                                            justifyContent: "space-between",
                                                            marginBottom: "10px",
                                                        }}
                                                    >
                                                        <Typography variant="h5">Tickets</Typography>
                                                        <Button
                                                            type="button"
                                                            variant="contained"
                                                            startIcon={<AddIcon />}
                                                            onClick={() =>
                                                                arrayHelpers.push({ issueDate: "", quantity: "", price: "" })
                                                            }
                                                        >
                                                            Add
                                                        </Button>
                                                    </div>
                                                </Grid>
                                                {values.tickets?.map((pd, index) => {
                                                    return (
                                                        <Grid
                                                            container
                                                            key={index}
                                                            spacing={4}
                                                            paddingLeft={2}
                                                        >
                                                            <Grid item xs={4}>
                                                                <Field
                                                                    as={TextField}
                                                                    fullWidth={true}
                                                                    name={`tickets.${index}.issueDate`}
                                                                    variant="standard"
                                                                      type='date'
                                                                      
                                                                    helperText={
                                                                        <ErrorMessage
                                                                            name={`tickets.${index}.issueDate`}
                                                                        />
                                                                    }
                                                                />
                                                            </Grid>
                                                            <Grid item xs={4}>
                                                                <Field
                                                                    as={TextField}
                                                                    fullWidth={true}
                                                                    name={`tickets.${index}.quantity`}
                                                                    label="Quantity"
                                                                    variant="standard"
                                                                    helperText={
                                                                        <ErrorMessage
                                                                            name={`tickets.${index}.quantity`}
                                                                        />
                                                                    }
                                                                />
                                                            </Grid>
                                                            <Grid item xs={3}>
                                                                <Field
                                                                    as={TextField}
                                                                    fullWidth={true}
                                                                    name={`tickets.${index}.price`}
                                                                    label="Price"
                                                                    variant="standard"
                                                                    helperText={
                                                                        <ErrorMessage
                                                                            name={`tickets.${index}.price`}
                                                                        />
                                                                    }
                                                                />
                                                            </Grid>
                                                            <Grid item xs={1}>
                                                                <IconButton
                                                                    onClick={() => arrayHelpers.remove(index)}
                                                                >
                                                                    <DeleteIcon />
                                                                </IconButton>
                                                            </Grid>
                                                        </Grid>
                                                    );
                                                })}
                                            </>
                                        );
                                    }}
                                </FieldArray>
                                <Grid item xs={12} style={{
                                    display: "flex",
                                    justifyContent: "space-between"
                                }}>

                                    <Button type="submit" color="primary" variant="contained">
                                        Submit
                                    </Button>
                                    <Button href="/Visitors" color="primary" variant="contained">
                                        Back to Visitor list
                                    </Button>
                                </Grid>
                            </Grid>

                        </Form>
                    );
                }}
            </Formik>
            <Snackbar
                open={notificationOpen}
                autoHideDuration={2000}
                message={message}
                action={"Undo"}
                onClose={handleClose}
            />
        </Container>
    );
}
export default VisitorEditForm;