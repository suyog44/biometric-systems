#include <TutorialUtils.h>

#include <NCore.h>
#include <NCluster.h>

const NChar title[] = N_T("ServerAdmin");
const NChar description[] = N_T("Demonstrates how to administrate matching server");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2008-2017 Neurotechnology");

char *server_ip = "127.0.0.1"; /**< Default server IP address*/
char *server_port = "24932"; /**< Default server port number */

/**
* Show usage screen to user.
*/
static int
usage()
{
	printf("usage: %s [options] -c [command] [-a [arguments] -o [optional]]\n", title);
	printf("\noptions:\n"
		"   -s <server IP>[:<server port>]\n"
		"\ncommand:\n"
		"   start                - Start cluster\n"
		"   stop <params>        - Stop (wait until finished task in progress) server (-a 0) or node (-a node number)\n"
		"   kill                 - Instantly stop server\n"
		"   info <params>        - Print info about cluster or nodes\n"
		"   dbflush              - DB flush\n"
		"   dbupdate             - DB update\n"
		"   dbchanged <params>   - Notify server of changed templates in DB\n"
		"\narguments:\n"
		"   server               - server\n"
		"   <id>                 - node ID\n"
		"\noptional:\n"
		"   complete     - complete running tasks information\n"
		"   short        - short information about running tasks\n"
		"\nexamples:\n");
	printf("   %s -s 127.0.0.1:24932 -c start \n", title);
	printf("   %s -s 127.0.0.1:24932 -c kill -a server \n", title);
	printf("   %s -s 127.0.0.1:24932 -c stop -a 3\n", title);
	printf("   %s -s 127.0.0.1:24932 -c info -a tasks -o complete\n", title);
	printf("   %s -s 127.0.0.1:24932 -c info -a tasks -o short\n", title);
	printf("   %s -s 127.0.0.1:24932 -c info -a nodes\n", title);
	printf("   %s -s 127.0.0.1:24932 -c info -a results\n", title);
	printf("   %s -s 127.0.0.1:24932 -c info -a server\n", title);
	printf("   %s -s 127.0.0.1:24932 -c dbupdate\n", title);
	printf("   %s -s 127.0.0.1:24932 -c dbchanged -a template_id\n", title);
	printf("   %s -s 127.0.0.1:24932 -c dbflush\n\n", title);

	return 1;
}

#define CHECKOPT() \
{ \
	if (optarg == NULL) \
{ \
	parse_error = 1; \
	goto breakloop; \
} \
	c++; \
}

/**
* Parses arguments from command line.
*/
static int
parse_args(int argc, char **argv, char **srv_ip, char **srv_port, char **cmd, char **carg, char **oarg)
{
	int c;
	int parse_error = 0;
	char *separator;

	if (argv == NULL || srv_ip == NULL || srv_port == NULL || cmd == NULL ||
		carg == NULL || oarg == NULL)
	{
		return CLUSTER_ARGUMENT_ERROR;
	}
	
	*srv_ip = (char*) malloc(strlen(server_ip) + 1);
	strncpy(*srv_ip, server_ip, strlen(server_ip));
	(*srv_ip)[strlen(server_ip)] = '\0';

	*srv_port = (char*) malloc(strlen(server_port) + 1);
	strncpy(*srv_port, server_port, strlen(server_port));
	(*srv_port)[strlen(server_port)] = '\0';

	*cmd = NULL;
	*carg = NULL;
	*oarg = NULL;

	for (c = 1; c < argc; c++)
	{
		char *optarg = NULL;

		if (strlen(argv[c]) != 2 || argv[c][0] != '-')
		{
			parse_error = 1;
			break;
		}

		if (argc > c+1 && argv[c+1][0] != '-')
		{
			optarg = argv[c+1]; // we have a parameter for given flag
		}

		switch (argv[c][1])
		{
		case 's':
			CHECKOPT();
			separator = strchr(optarg, ':');
			if (separator)
			{
				size_t addressLength;
				size_t portLength;

				addressLength = separator - optarg;
				portLength = strlen(optarg) - addressLength - 1;
				*srv_ip = (char*) malloc(addressLength + 1);
				strncpy(*srv_ip, optarg, addressLength);
				(*srv_ip)[addressLength] = '\0';
				*srv_port = (char*) malloc(portLength + 1);
				strncpy(*srv_port, optarg + addressLength + 1, portLength);
				(*srv_port)[portLength] = '\0';
			}
			else
			{
				*srv_ip = (char*) malloc(strlen(optarg) + 1);
				strncpy(*srv_ip, optarg, strlen(optarg));
				(*srv_ip)[strlen(optarg)] = '\0';
			}
			break;

		case 'c':
			CHECKOPT();
			*cmd = optarg;
			break;

		case 'a':
			CHECKOPT();
			*carg = optarg;
			break;

		case 'o':
			CHECKOPT();
			*oarg = optarg;
			break;

		default:
			parse_error = 1;
			goto breakloop;
		}

	} /*   while   */

breakloop:

	if (*cmd == NULL)
	{
		parse_error = 1;
	}

	if (parse_error)
	{
		usage();
		return CLUSTER_ARGUMENT_ERROR;
	}

	return CLUSTER_OK;
}

/**
* Print cluster running tasks information
*/
static void
print_short_running_tasks_info(admin_handle_t *results)
{
	cluster_status_code_e ret;
	int count;
	int i;
	int task_id;
	int nodes_completed;
	int working_nodes;

	ret = cluster_get_admin_task_short_info_count(results, &count);
	if (ret != CLUSTER_OK)
	{
		printf("failed to extract short information about matching tasks\n");
		return;
	}

	if (count == 0)
	{
		printf("no active matching task on server\n");
	}
	else
	{
		printf("active tasks on server: %d \n",count);

		for (i = 0; i < count; i++)
		{
			ret = cluster_get_admin_tasks_short_info(results, i, &task_id, &nodes_completed, &working_nodes);
			if (ret == 0)
			{
				printf("task id: %d \n", task_id);
				printf("nodes already completed task: %d \n", nodes_completed);
				printf("amount of nodes working on the task: %d \n", working_nodes);
			}
		}
	}
}

/**
* Print complete running tasks information
*/
static void
print_complete_running_tasks_info(admin_handle_t *results)
{
	cluster_status_code_e ret;
	int count;
	int i, k;

	ret = cluster_get_admin_tasks_complete_info_count(results, &count);
	if (ret != CLUSTER_OK)
	{
		printf("failed to extract information about matching tasks (error %i)\n", ret);
		return;
	}

	if (count == 0)
	{
		printf("no active matching tasks on server\n");
	}
	else
	{
		int task_id;
		int nodes_completed;
		int working_nodes;
		int task_progress;
		int node_id;
		int node_progress;

		printf("active tasks on server: %d \n", count);

		for (i = 0; i < count; i++)
		{
			ret = cluster_get_admin_tasks_complete_info(results, i, &task_id, &nodes_completed, &working_nodes, &task_progress);
			if (ret != 0)
			{
				continue;
			}

			printf("task id: %d \n", task_id);
			printf("nodes already completed task: %d \n", nodes_completed);
			printf("amount of nodes working on the task: %d \n", working_nodes);
			printf("progress of the task: %d%%\n", task_progress);
			printf("nodes working on the task and progress of the task in nodes:\n");

			for(k = 0; k < working_nodes; k++)
			{
				ret = cluster_get_admin_tasks_complete_info_node_info(results, i, k, &node_id, &node_progress);
				if (ret != 0)
				{
					continue;
				}
				printf("%d\t %d%%\n", node_id, node_progress);
			}
			printf("\n");
		}
	}
}

/**
* Print nodes info
*/
static void
print_nodes_info(admin_handle_t *results)
{
	cluster_status_code_e ret;
	int nodes_count;
	int starting = 0;
	int removing = 0;
	int i, k;
	int node_id;
	cluster_node_state_e node_state;

	ret = cluster_get_admin_nodes_info_count(results, &nodes_count);
	if (ret != CLUSTER_OK)
	{
		printf("failed to ectract information about cluster nodes");
		return;
	}

	if (nodes_count == 0)
	{
		printf("cluster has no nodes started\n");
		return;
	}

	printf("active nodes on server: %d \n", nodes_count);

	printf("cluster nodes ids:");
	for (i = 0; i < nodes_count; i++)
	{
		if (i % 8 == 0)
		{
			printf("\n");
		}

		ret = cluster_get_admin_nodes_info(results, i, &node_id, &node_state);
		if (ret != 0)
		{
			continue;
		}

		if (node_state == CLUSTER_NODE_READY)
		{
			printf("%d\n", node_id);
		}
		else if (node_state == CLUSTER_NODE_STARTING)
		{
			starting++;
		}
		else if (node_state == CLUSTER_NODE_REMOVING)
		{
			removing++;
		}
	}

	if (starting)
	{
		printf("\nnodes recently joined to cluster: %d", starting);
		k = 0;
		for (i = 0; i < nodes_count; i++)
		{
			ret = cluster_get_admin_nodes_info(results, i, &node_id, &node_state);
			if (ret != 0)
			{
				continue;
			}

			if (node_state == CLUSTER_NODE_STARTING)
			{
				if (k % 8 == 0)
				{
					printf("\n");
				}
				printf("%d\t", node_id);
				k++;
			}
		}

	}

	if (removing)
	{
		printf("\nnodes ready for removing from cluster %d", removing);
		k = 0;
		for (i = 0; i < nodes_count; i++)
		{
			ret = cluster_get_admin_nodes_info(results, i, &node_id, &node_state);
			if (ret != 0)
			{
				continue;
			}

			if (node_state == CLUSTER_NODE_REMOVING)
			{
				if (k % 8 == 0)
				{
					printf("\n");
				}
				printf("%d\t", node_id);
				k++;
			}
		}
	}
}

/**
* Print cluster maching tasks results info
*/
static void
print_tasks_results_info(admin_handle_t *results)
{
	int i;
	int result_id;
	int results_count;
	cluster_status_code_e ret;

	ret = cluster_get_admin_results_count(results, &results_count);
	if (ret != CLUSTER_OK)
	{
		printf("failed to extract information from packet");
		return;
	}

	if (results_count== 0)
	{
		printf("cluster has no pending results\n");
		return;
	}

	printf("cluster has %d task results ready for retrieval or deletion", results_count);
	for (i = 0; i < results_count; i++)
	{
		if (i % 8 == 0)
		{
			printf("\n");
		}

		ret = cluster_get_admin_results_id(results, i, &result_id);
		if (ret == 0)
		{
			printf("%d\t", result_id);
		}
	}
	printf("\n\n");
}

/**
* Print server info
*/
static void
print_server_info(admin_handle_t *results)
{
	int server_status;
	cluster_status_code_e ret;

	ret = cluster_admin_get_server_info(results, &server_status);
	if (ret != CLUSTER_OK)
	{
		printf("Failed to extract information from packet");
		return;
	}

	switch (server_status)
	{
	case CLUSTER_SERVER_STATUS_PREPARING:
		printf("Preparing\n");
		break;

	case CLUSTER_SERVER_STATUS_READY:
		printf("Ready\n");
		break;

	case CLUSTER_SERVER_STATUS_ERROR:
		printf("Error\n");
		break;

	default:
		printf("Unknown: %d\n", server_status);
		break;
	}
}

/**
* Print received results
*/
static void
print_result(admin_handle_t *results, int packet_type)
{
	switch (packet_type)
	{
	case CLUSTER_PACKET_ADMIN_COMPLETE_INFO:
		{
			print_complete_running_tasks_info(results);
			break;
		}

	case CLUSTER_PACKET_ADMIN_SHORT_INFO:
		{
			print_short_running_tasks_info(results);
			break;
		}

	case CLUSTER_PACKET_ADMIN_NODES_INFO:
		{
			print_nodes_info(results);
			break;
		}

	case CLUSTER_PACKET_ADMIN_RESULTS_INFO:
		{
			print_tasks_results_info(results);
			break;
		}

	case CLUSTER_PACKET_ADMIN_SERVER_INFO:
		{
			print_server_info(results);
			break;
		}
	}
}

/**
* Check if necessary to receive response from server
* @return non zero if necessary to get results from server
*/
static int
need_receive(char * cmd)
{
	return strcmp(cmd, "info") == 0;
}

/**
* search for node id.
*/
static int
get_info_type(char *arg, char *oarg)
{
	int	ret;

	if (arg == NULL)
	{
		printf("arguments required\n");
		return -1;
	}

	if ((ret = strcmp(arg, "tasks")) == 0)
	{
		if (oarg == NULL)
		{
			oarg = "short";
		}

		if ((ret = strcmp(oarg, "complete")) == 0)
		{
			return CLUSTER_PACKET_ADMIN_COMPLETE_INFO;
		}
		else if ((ret = strcmp(oarg, "short")) == 0)
		{
			return CLUSTER_PACKET_ADMIN_SHORT_INFO;
		}
		else
		{
			printf("unsupported information type\n");
			return -1;
		}

	}
	else if ((ret = strcmp(arg, "nodes")) == 0)
	{
		return CLUSTER_PACKET_ADMIN_NODES_INFO;
	}
	else if ((ret = strcmp(arg, "results")) == 0)
	{
		return CLUSTER_PACKET_ADMIN_RESULTS_INFO;
	}
	else if ((ret = strcmp(arg, "server")) == 0)
	{
		return CLUSTER_PACKET_ADMIN_SERVER_INFO;
	}
	else
	{
		printf("unsupported information type\n");
		return -1;
	}
}

/**
* search for node id.
*/
static int
get_node_id(char *id)
{
	int	ret;
	char	*tmp;

	if (id == NULL)
	{
		printf("arguments required\n");
		return -1;
	}

	ret = strcmp(id, "server");
	if (ret != 0)
	{
		ret = strtol(id, &tmp, 10);
		if (tmp == id)
		{
			return -1;
		}
	}

	return ret;
}

/**
* Parsing user suplied parameters, and building packet
*/
static admin_handle_t*
packet_build(char *cmd, char *arg, char *oarg)
{
	admin_handle_t *packet = NULL;
	int	node_id;
	int type;

	packet = initialize_admin_handle();

	if (strcmp(cmd, "start") == 0)
	{
		if (cluster_admin_cluster_start(packet) == 0)
		{
			return packet;
		}
		else
		{
			return NULL;
		}
	}
	else if (strcmp(cmd, "stop") == 0)
	{
		node_id = get_node_id(arg);
		if (node_id  == -1)
		{
			printf("wrong parameter\n");
			return NULL;
		}
		if (cluster_admin_node_stop(packet, node_id) == 0)
		{
			return packet;
		}
		else
		{
			return NULL;
		}
	}
	else if (strcmp(cmd, "kill") == 0)
	{
		node_id = get_node_id(arg);
		if (node_id == -1)
		{
			printf("wrong parameter for kill command\n");
			return NULL;
		}

		if (node_id == 0)
		{
			if (cluster_admin_server_kill(packet) == 0)
			{
				return packet;
			}
			else
			{
				return NULL;
			}
		}
		else
		{
			if (cluster_admin_node_kill(packet, node_id) == 0)
			{
				return packet;
			}
			else
			{
				return NULL;
			}
		}
	}
	else if (strcmp(cmd, "info") == 0)
	{
		type = get_info_type(arg, oarg);
		if (type == -1)
		{
			return NULL;
		}

		switch (type)
		{
		case CLUSTER_PACKET_ADMIN_COMPLETE_INFO:
			if (cluster_admin_tasks_complete_info_req(packet) == 0)
			{
				return packet;
			}
			break;
		case CLUSTER_PACKET_ADMIN_SHORT_INFO:
			if (cluster_admin_tasks_short_info_req(packet) == 0)
			{
				return packet;
			}
			break;
		case CLUSTER_PACKET_ADMIN_RESULTS_INFO:
			if (cluster_admin_results_info_req(packet) == 0)
			{
				return packet;
			}
			break;
		case CLUSTER_PACKET_ADMIN_NODES_INFO:
			if (cluster_admin_nodes_info_req(packet) == 0)
			{
				return packet;
			}
			break;
		case CLUSTER_PACKET_ADMIN_SERVER_INFO:
			if (cluster_admin_server_info_req(packet) == 0)
			{
				return packet;
			}
			break;
		}

		return NULL;
	}
	else if (strcmp(cmd, "dbupdate") == 0)
	{
		if (cluster_admin_database_update(packet) == 0)
		{
			return packet;
		}
		else
		{
			return NULL;
		}
	}
	if (strcmp(cmd, "dbchanged") == 0)
	{
		char	*id_strings[] = { NULL, NULL };
		int count = 0;

		if (arg == NULL)
		{
			printf("dbchanged command requires ID if changed record(s) as argument\n");
			return NULL;
		}
		id_strings[0] = arg;

		while (id_strings[count])
		{
			count++;
		}

		if (cluster_admin_change_db_records(packet, count, (const char **) id_strings) == 0)
		{
			return packet;
		}
		else
		{
			return NULL;
		}
	}
	else if (strcmp(cmd, "dbflush") == 0)
	{
		if (cluster_admin_database_flush(packet) == 0)
		{
			return packet;
		}
		else
		{
			return NULL;
		}
	}

	printf("unrecognized command, see usage for list of valid strings\n");
	return NULL;
}

/**
* Main administrator demonstrating code
*/
int
main(int argc, char *argv[])
{
	char *carg;
	char *oarg;
	char *cmd;
	NResult res;
	cluster_packet_type_e packet_type;
	cluster_socket_t *sock = NULL;
	admin_handle_t *packet = NULL, *results = NULL;
	cluster_status_code_e ret;

	char *srv_ip = NULL;
	char *srv_port = NULL;

	OnStart(title, description, version, copyright, argc, argv);

	ret = parse_args(argc, argv, &srv_ip, &srv_port, &cmd, &carg, &oarg);
	if (ret != CLUSTER_OK)
	{
		printf("could not parse args\n");
		OnExit();
		return ret;
	}

	ret = cluster_initialize();
	if (ret != CLUSTER_OK)
	{
		printf("cluster_initialize failed with error %d\n", ret);
		res = ret;
		goto FINALLY;
	}

	packet = packet_build(cmd, carg, oarg);
	if (packet == NULL)
	{
		printf("packet not built\n");
		res = N_E_FAILED;
		goto FINALLY;
	}

	sock = cluster_sock_connect(&ret, srv_ip, srv_port);
	if (sock == NULL || ret != 0)
	{
		printf("could not connect to server\n");
		res = ret;
		goto FINALLY;
	}

	free(srv_ip);
	srv_ip = NULL;
	free(srv_port);
	srv_port = NULL;

	ret = cluster_packet_send(packet, sock);
	if (ret != CLUSTER_OK)
	{
		printf("packet not sent\n");
		res = ret;
		goto FINALLY;
	}

	/* Receive packet from server if it's necessary */
	if (need_receive(cmd))
	{
		results = initialize_admin_handle();
		if (results == NULL)
		{
			res = N_E_FAILED;
			goto FINALLY;
		}

		ret = cluster_packet_recv(results, sock);
		if (ret!= CLUSTER_OK)
		{
			printf("got no results\n");
			res = ret;
			goto FINALLY;
		}

		ret = cluster_get_packet_type(results, &packet_type);
		if (ret!= CLUSTER_OK)
		{
			printf("got no result type\n");
			res = ret;
			goto FINALLY;
		}

		print_result(results, packet_type);
		finalize_admin_handle(results);
		results = NULL;
	}

	res = N_OK;

FINALLY:
	if (srv_ip)
		free(srv_ip);
	if (srv_port)
		free(srv_port);
	if (results)
		finalize_admin_handle(results);
	if (packet)
		finalize_admin_handle(packet);
	if (sock)
		cluster_sock_close(sock);

	cluster_finalize();

	OnExit();

	return res;
}
