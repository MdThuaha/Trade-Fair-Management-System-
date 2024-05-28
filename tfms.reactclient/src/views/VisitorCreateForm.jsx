import {
    Button,
    Container,
    FormHelperText,
    Grid,
    IconButton,
    InputAdornment,
    InputLabel,
    MenuItem,
    Select,
    TextField,
    Typography,
    Snackbar
} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import DeleteIcon from "@mui/icons-material/Delete";
import { ErrorMessage, Field, FieldArray, Form, Formik } from "formik";
import { useEffect, useState, useRef } from "react";
import * as Yup from "yup";
import { getGenderOptions, postVisitor } from "../services/VisitorService";

const VisitorCreateForm = () => {
    const [notificationOpen, setNotificationOpen] = useState(false);
    const [gender, setGender] = useState([]);
    const [message, setMessage] = useState("");

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
        firstName: "",
        lastName: "",
        email: "",
        phone: "",
        gender: '',
        nationality: "",

        tickets: [
            {
                issueDate: "",
                quantity: "",
                price: "",
            },
        ],
    };

    useEffect(() => {
        fetchGenders();
    }, []);
    const fetchGenders = async () => {
        const { data } = await getGenderOptions()
        setGender(data);
        console.log(data);
    }


    const handleSubmit = async (values, { setSubmitting, resetForm }) => {
        try {
            let visitor = {
                firstName: values.firstName,
                lastName: values.lastName,
                email: values.email,
                phone: values.phone,
                gender: values.gender,
                nationality: values.nationality,
                tickets: [...values.tickets]
            };
            console.log(visitor);
            await postVisitor(visitor);
            resetForm();
            navigate('/Visitors');
        } catch (error) {
            console.error(error);
        } finally {
            setSubmitting(false);
        }
        setMessage("Data Save successfully");
        setNotificationOpen(true);
    };
    const handleClose = () => {
        setNotificationOpen(false);
    };

    return (
        <Container>
            <Formik
                initialValues={initialValues}
                validationSchema={validationSchema}
                onSubmit={handleSubmit}
            >
                {({ isSubmitting, setFieldValue, values, handleChange }) => {
                    return (
                        <Form>
                            <Grid container spacing={2}>
                                <Grid item xs={12}>
                                    <Typography variant="h4">Visitors form with Tickets</Typography>
                                </Grid>

                                <Grid item xs={6}>
                                    <Field
                                        as={TextField}
                                        fullWidth={true}
                                        name="firstName"
                                        label="First Name"
                                        variant="standard"
                                        helperText={<ErrorMessage name="firstName" />}
                                    />
                                </Grid>
                                <Grid item xs={5}>
                                    <Field
                                        as={TextField}
                                        fullWidth={true}
                                        name="lastName"
                                        label="Last Name"
                                        variant="standard"
                                        helperText={<ErrorMessage name="lastName" />}
                                    />
                                </Grid>
                                <Grid item xs={6}>
                                    <Field
                                        as={TextField}
                                        fullWidth={true}
                                        name="email"
                                        label="Email"
                                        variant="standard"
                                        helperText={<ErrorMessage name="email" />}
                                    />
                                </Grid>
                                <Grid item xs={5}>
                                    <Field
                                        as={TextField}
                                        fullWidth={true}
                                        name="phone"
                                        label="Phone"
                                        variant="standard"
                                        helperText={<ErrorMessage name="phone" />}
                                    />
                                </Grid>

                                <Grid item xs={6}>
                                    <InputLabel id='catId'>Gender</InputLabel>
                                    <Field
                                        as={Select}
                                        fullWidth={true}
                                        name="gender"
                                        labelId="catId"
                                        variant="standard"
                                    >
                                        <MenuItem value={1}>Male</MenuItem>
                                        <MenuItem value={2}>Female</MenuItem>
                                        <MenuItem value={3}>Others</MenuItem>
                                    </Field>
                                    <FormHelperText><ErrorMessage name="gender" /></FormHelperText>
                                </Grid>



                                <Grid item xs={5}>
                                    <Field
                                        as={TextField}
                                        fullWidth={true}
                                        name="nationality"
                                        label="Nationality"
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
                                                            paddingRight: "25px",
                                                        }}
                                                    >
                                                        <Typography variant="h5">Tickets</Typography>
                                                        <Button type="button"
                                                            variant="contained"
                                                            startIcon={<AddIcon />}
                                                            onClick={() => arrayHelpers.push({ issueDate: '', quantity: '', price: '' })}
                                                        >
                                                            Add
                                                        </Button>
                                                    </div>
                                                </Grid>
                                                {values.tickets.map((pd, index) => {
                                                    return (
                                                        <Grid
                                                            container
                                                            key={index}
                                                            spacing={4}
                                                            paddingLeft={2}
                                                            paddingRight={2}
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
                                                            <Grid item xs={3}>
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

                                                            <Grid item xs={4}>
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
                                <Grid item xs={11} style={{
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
};

export default VisitorCreateForm;